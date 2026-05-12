# scripts/recommendation.py
import os
import sqlite3
from fastapi import FastAPI, HTTPException, Query

DB_PATH = os.path.join(os.path.dirname(__file__), "..", "backend", "cinema.db")

app = FastAPI()

def _rows_to_movies(rows):
    return [
        {
            "id": row["Id"],
            "title": row["Title"],
            "genre": row["Genre"],
            "durationMinutes": row["DurationMinutes"],
            "description": row["Description"],
            "posterUrl": row["PosterUrl"],
        }
        for row in rows
    ]

def _query(conn, sql, params):
    cur = conn.cursor()
    cur.execute(sql, params)
    return cur.fetchall()

@app.get("/recommendations/{genre}")
def get_recommendations(
    genre: str,
    limit: int = Query(3, ge=1, le=10),
    preferUpcoming: bool = True
):
    if not os.path.exists(DB_PATH):
        raise HTTPException(status_code=500, detail="Database not found.")

    conn = sqlite3.connect(DB_PATH)
    conn.row_factory = sqlite3.Row

    movies = []

    if preferUpcoming:
        rows = _query(
            conn,
            """
            SELECT DISTINCT m.Id, m.Title, m.Genre, m.DurationMinutes, m.Description, m.PosterUrl
            FROM Movies m
            JOIN Sessions s ON s.MovieId = m.Id
            WHERE LOWER(m.Genre) = LOWER(?)
              AND s.StartTime > datetime('now')
            ORDER BY RANDOM()
            LIMIT ?
            """,
            (genre, limit)
        )
        movies.extend(_rows_to_movies(rows))

    if len(movies) < limit:
        rows = _query(
            conn,
            """
            SELECT Id, Title, Genre, DurationMinutes, Description, PosterUrl
            FROM Movies
            WHERE LOWER(Genre) = LOWER(?)
              AND Id NOT IN ({})
            ORDER BY RANDOM()
            LIMIT ?
            """.format(",".join(["?"] * len(movies)) if movies else "0"),
            [genre] + [m["id"] for m in movies] + [limit - len(movies)]
        )
        movies.extend(_rows_to_movies(rows))

    conn.close()

    if not movies:
        raise HTTPException(status_code=404, detail="No movies found.")
    return movies