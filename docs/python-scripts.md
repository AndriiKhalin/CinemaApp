# Python Scripts: Seed + Recommendations

## Requirements
- Python 3.13
- Internet access for TMDB
- TMDB API key

## Install dependencies (one time)
py -3.13 -m pip install requests fastapi uvicorn

## Set TMDB key
### PowerShell (current session only)
$env:TMDB_KEY="YOUR_KEY_HERE"

### Permanent (new terminals only)
setx TMDB_KEY "YOUR_KEY_HERE"
# close and reopen terminal

## Seed database from TMDB (Now Playing)
py -3.13 scripts/seed.py --refresh --count 40

## Run recommendation service
py -3.13 -m uvicorn scripts.recommendation:app --reload

## Test
- GET http://localhost:8000/recommendations/Action
- GET https://localhost:7200/api/recommendations/Action