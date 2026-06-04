# Render deploy

Use this repository as a Render Docker web service.

## Render settings

- Root Directory: leave empty if the repository root is `web-project_backend`.
- Runtime: Docker
- Plan: Free
- Health Check Path: `/health`

If Render does not read `render.yaml`, use these commands manually:

```bash
docker build -t web-project-backend .
docker run -p 8080:8080 -e PORT=8080 web-project-backend
```

## Environment variables

Set these in Render:

```text
ASPNETCORE_ENVIRONMENT=Production
FRONTEND_URLS=https://your-vercel-project.vercel.app
ConnectionStrings__DefaultConnection=Host=...;Port=5432;Database=...;Username=...;Password=...;SSL Mode=Require
Jwt__Key=your-super-secret-key-that-is-at-least-32-characters-long
Jwt__Issuer=https://your-render-service.onrender.com
Jwt__Audience=https://your-render-service.onrender.com
```

After the backend is deployed, set this variable in Vercel for the frontend:

```text
VITE_API_URL=https://your-render-service.onrender.com/api
```
