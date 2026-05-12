# scripts/seed.py
import os
import sqlite3
import requests
import argparse
import random
from datetime import datetime, timedelta

DB_PATH = os.path.join(os.path.dirname(__file__), "..", "backend", "cinema.db")
TMDB_KEY = os.getenv("TMDB_KEY")
TMDB_BASE = "https://api.themoviedb.org/3"
POSTER_BASE = "https://image.tmdb.org/t/p/w500"

def tmdb_get(path, params=None):
    if not TMDB_KEY:
        raise RuntimeError("TMDB_KEY is not set.")
    params = params or {}
    params["api_key"] = TMDB_KEY
    resp = requests.get(f"{TMDB_BASE}{path}", params=params, timeout=8)
    resp.raise_for_status()
    return resp.json()

def ensure_cinema_and_halls(cur):
    cur.execute("SELECT Id FROM Cinemas LIMIT 1")
    row = cur.fetchone()
    if row:
        cinema_id = row[0]
    else:
        cur.execute("INSERT INTO Cinemas (Name, Address) VALUES (?, ?)", ("Cinema One", "123 Main St"))
        cinema_id = cur.lastrowid

    cur.execute("SELECT Id FROM Halls WHERE CinemaId = ?", (cinema_id,))
    halls = [r[0] for r in cur.fetchall()]
    if halls:
        return halls

    cur.execute("INSERT INTO Halls (CinemaId, Name, TotalRows, SeatsPerRow) VALUES (?, ?, ?, ?)",
                (cinema_id, "Hall A", 10, 12))
    hall_a = cur.lastrowid
    cur.execute("INSERT INTO Halls (CinemaId, Name, TotalRows, SeatsPerRow) VALUES (?, ?, ?, ?)",
                (cinema_id, "Hall B", 8, 10))
    hall_b = cur.lastrowid
    return [hall_a, hall_b]

def upsert_movie(cur, movie, genre_map):
    tmdb_id = movie["id"]
    title = movie["title"]
    description = movie.get("overview") or ""
    poster_path = movie.get("poster_path")
    poster_url = f"{POSTER_BASE}{poster_path}" if poster_path else ""
    genre_ids = movie.get("genre_ids") or []
    genre = genre_map.get(genre_ids[0], "Unknown") if genre_ids else "Unknown"

    cur.execute("SELECT Id FROM Movies WHERE TmdbId = ?", (tmdb_id,))
    row = cur.fetchone()
    if row:
        return row[0], False

    cur.execute(
        "INSERT INTO Movies (Title, Genre, DurationMinutes, Description, PosterUrl, TmdbId) VALUES (?, ?, ?, ?, ?, ?)",
        (title, genre, random.randint(90, 150), description, poster_url, tmdb_id)
    )
    return cur.lastrowid, True

def create_sessions(cur, movie_id, hall_ids, days=14, count=2):
    now = datetime.now()
    for _ in range(count):
        day = random.randint(0, days)
        hour = random.choice([10, 12, 14, 16, 18, 20])
        start_time = now + timedelta(days=day, hours=hour - now.hour)
        hall_id = random.choice(hall_ids)
        price = random.choice([7.50, 9.50, 11.00])
        cur.execute(
            "INSERT INTO Sessions (MovieId, HallId, StartTime, TicketPrice) VALUES (?, ?, ?, ?)",
            (movie_id, hall_id, start_time.isoformat(), price)
        )

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--count", type=int, default=40)
    parser.add_argument("--refresh", action="store_true")
    parser.add_argument("--sessions-per-movie", type=int, default=2)
    args = parser.parse_args()

    if not os.path.exists(DB_PATH):
        raise FileNotFoundError(f"DB not found at: {DB_PATH}")

    conn = sqlite3.connect(DB_PATH)
    cur = conn.cursor()

    cur.execute("SELECT COUNT(*) FROM Movies")
    movies_count = cur.fetchone()[0]
    if movies_count > 0 and not args.refresh:
        print("Movies already seeded. Use --refresh to update.")
        conn.close()
        return

    halls = ensure_cinema_and_halls(cur)

    genre_data = tmdb_get("/genre/movie/list")
    genre_map = {g["id"]: g["name"] for g in genre_data.get("genres", [])}

    # Fetch now playing movies (paged)
    movies = []
    page = 1
    while len(movies) < args.count:
        data = tmdb_get("/movie/now_playing", {"page": page})
        movies.extend(data.get("results", []))
        if page >= data.get("total_pages", 1):
            break
        page += 1

    movies = movies[:args.count]

    added = 0
    for m in movies:
        movie_id, created = upsert_movie(cur, m, genre_map)
        if created:
            create_sessions(cur, movie_id, halls, count=args.sessions_per_movie)
            added += 1

    conn.commit()
    conn.close()
    print(f"Seed complete. Added {added} new movies.")

if __name__ == "__main__":
    main()