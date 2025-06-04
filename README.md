# Bozeman Bike Trails Web Application

A simple web app to log and visualize bike trails in the Bozeman area. Upload GPX files, store ride data, and view your trails on an interactive 3D map.

---

## Features

- **GPX Upload & Storage**: Upload `.gpx` files and convert them to GeoJSON for storage in a local SQLite database.
- **3D Trail Map**: Visualize rides using Mapbox GL JS with terrain and route overlays.
- **Ride Listing**: Paginated home page with ride details and metadata.
- **Lightweight UI**: Built with Razor Pages and Bootstrap 5.

---

## Tech Stack

- **Backend**: ASP.NET Core (.NET 7), Razor Pages, EF Core (SQLite)
- **Frontend**: Mapbox GL JS, Bootstrap 5, toGeoJSON
- **Database**: SQLServer with EF Core ORM

---

## Setup

1. Clone the repo:
   ```bash
   git clone https://github.com/joshaney324/bike_trail_webapp.git
   cd bike_trail_webapp
   ```

2. Restore dependencies and run:
   ```bash
   dotnet restore
   dotnet run
   ```

3. Navigate to:
   ```
   https://localhost:5001
   ```

---

## Configuration

- Add your **Mapbox Access Token** in `appsettings.json` under `"Mapbox:AccessToken"`.
- Default database is `Data/bike_trails.db` (auto-created on first run).

---

## Future Plans

- Add user accounts
- Server-side GPX parsing
- Trail filtering, elevation stats, and heatmaps
- Cloud hosting with PostgreSQL + Docker

---

## Contact

**Josh Aney**  
GitHub: [@joshaney324](https://github.com/joshaney324)  
Email: josh.aney@icloud.com
