# AeroSentinel Frontend V2

Standalone React, TypeScript, and Tailwind CSS admin panel for the AeroSentinel backend.

## Run

```bash
npm install
npm run dev
```

The app uses `http://localhost:5253` for the backend by default. Override it with:

```bash
VITE_API_BASE_URL=http://localhost:5253 npm run dev
```

## Pages

- Dashboard
- Analytics
- Settings
- Waypoints
- Telemetry Logs

Live backend endpoints are used for `/dashboard`, `/waypoints`, and `/telemetry`. When the API is unavailable, the interface falls back to typed sample data so the panel remains usable during frontend work.
