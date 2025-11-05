# net-todo-api (ASP.NET Core + React + Vite)

A full‑stack Todo app with an ASP.NET Core Web API and a React + TypeScript client (Vite). This doc is a compact reference for setup, architecture, and conventions.

## Overview

- Backend (TodoApi): ASP.NET Core Web API, EF Core (SQL Server), DI with repository/service layers, AutoMapper, Swagger in dev, CORS for the React dev server.
  - Entry/config: `TodoApi/Program.cs`
  - Health endpoints: `GET/HEAD /healthz` and `/health` via `TodoApi/Controllers/HealthController.cs`
  - Data: `TodoApi/Models/Todo.cs` (domain), `TodoApi/Models/UpdateTodoDto.cs` (update DTO)
  - DI: `ITodoRepository` → `TodoRepository`, `ITodoService` → `TodoService` are registered in Program.cs

- Frontend (react-todo-client): React + TS + Vite, React Router, Tailwind CSS (via @tailwindcss/vite), utility components.
  - Entry: `src/main.tsx`, Routes: `src/App.tsx` (HomePage, TodoFormPage)
  - Types: `src/types/todo.ts` (Todo, CreateTodoDto, UpdateTodoDto)
  - Components: `src/components/AddTodoForm.tsx`, `src/components/Layout.tsx`, UI primitives under `src/components/ui/*`
  - Styling: `src/index.css` sets CSS variables + Tailwind layers; dark mode variant is declared with `@custom-variant dark`

## Repository layout

- `TodoApi/` — ASP.NET Core Web API
- `react-todo-client/` — React + Vite client
- `run.sh` — Dev helper to launch API and client together with a health check
- `tree.sh` — Utility to print a pruned tree (ignores bin/obj/node_modules/etc.)

## Backend details (TodoApi)

- Composition: see [Program.cs](TodoApi/Program.cs). Key points:
  - CORS policy `AllowDevOrigin` allows `http://localhost:5173`
  - EF Core SQL Server DbContext: `TodoDbContext` uses `DefaultConnection` from configuration
  - DI: `ITodoRepository` → `TodoRepository`, `ITodoService` → `TodoService`
  - Swagger enabled in development
  - HTTPS redirection only outside development
  - Controllers are mapped with `app.MapControllers()`

- Health endpoints: [Controllers/HealthController.cs](TodoApi/Controllers/HealthController.cs)
  - `GET/HEAD /healthz` and `GET/HEAD /health` return `{ status: "ok" }`

- Domain and DTOs:
  - [Models/Todo.cs](TodoApi/Models/Todo.cs) — `Todo` with `Id`, `Title`, `IsComplete`, `Description`, `User` (based on in-file comments)
  - [Models/UpdateTodoDto.cs](TodoApi/Models/UpdateTodoDto.cs) — update payload: `Title`, `Description?`, `IsComplete`

- Configuration:
  - Connection string: configure `DefaultConnection` in `TodoApi/appsettings.json` or environment
  - AutoMapper: `builder.Services.AddAutoMapper(typeof(Program))` (profiles are discovered by assembly scanning)

## Frontend details (react-todo-client)

- App shell and routing: [src/App.tsx](react-todo-client/src/App.tsx)
  - Routes:
    - `/` → `HomePage`
    - `/create` → `TodoFormPage` (mode="create")
    - `/edit/:id` → `TodoFormPage` (mode="edit")

- Types: [src/types/todo.ts](react-todo-client/src/types/todo.ts)
  - `Todo`, `CreateTodoDto` (title, optional description), `UpdateTodoDto` (title, optional description, isComplete)

- Add form: [src/components/AddTodoForm.tsx](react-todo-client/src/components/AddTodoForm.tsx)
  - Local state for `title` and `description`; prevents submit on empty/whitespace title; calls `onSubmit({ title, description })`

- Layout and UI:
  - [src/components/Layout.tsx](react-todo-client/src/components/Layout.tsx) — container wrapper
  - [src/components/ui/card.tsx](react-todo-client/src/components/ui/card.tsx), [src/components/ui/label.tsx](react-todo-client/src/components/ui/label.tsx) — Tailwind-based primitives
  - Tailwind + theme tokens in [src/index.css](react-todo-client/src/index.css)
  - Vite alias `@` → `src` in [vite.config.ts](react-todo-client/vite.config.ts)

## How to run (macOS)

Prereqs:
- .NET SDK 8+ (or the version your project targets)
- Node.js 18+ (recommended LTS)
- SQL Server (reachable from your machine). On macOS, easiest via Docker.

1) One command (API + client):
- Ensure `TodoApi` has a valid `DefaultConnection` (see DB section below).
- From repo root:
  - `chmod +x ./run.sh`
  - `./run.sh`
- Defaults:
  - API on http://localhost:5131
  - Client on http://localhost:5173
  - Health check: http://localhost:5131/healthz

Environment for `run.sh`:
- `API_PORT` (default `5131`)
- `API_WAIT_TIMEOUT_MS` (default `30000`)

2) Run separately:
- API:
  - `cd TodoApi`
  - `ASPNETCORE_URLS=http://localhost:5131 dotnet run --project ./TodoApi.csproj`
- Client:
  - `cd react-todo-client`
  - `npm install`
  - `npm run dev` (Vite on http://localhost:5173)

## Database setup (SQL Server)

- Configure `DefaultConnection` in `TodoApi/appsettings.json` or via env var `ConnectionStrings__DefaultConnection`.
- Example (local Docker):
  - Run SQL Server:
    - `docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStr0ng!Pass" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest`
  - Connection string:
    - `Server=localhost,1433;Database=TodoDb;User ID=sa;Password=YourStr0ng!Pass;TrustServerCertificate=True;`

Note: Ensure the database exists or EF migrations are configured to create it. Update CORS origin in [Program.cs](TodoApi/Program.cs) if you change the client port/origin.

## Useful endpoints and tooling

- API health: `GET /healthz` or `/health`
- Swagger UI (development): `GET /swagger`
- CORS policy name: `AllowDevOrigin` (change origins in one place)
- Client alias imports: `@/` → `src/...`

## Conventions and notes

- DTO vs domain: API exposes DTOs (e.g., `UpdateTodoDto`) and maps to domain model (`Todo`) via AutoMapper.
- Client/server contracts: mirrored in client types (`CreateTodoDto`, `UpdateTodoDto`) under `react-todo-client/src/types/todo.ts`. Keep these in sync with API DTOs.
- Styling: design tokens defined as CSS custom properties; Tailwind utilities applied via `@layer base` in `index.css`.

## Next steps / TODOs

- Verify/implement Todo CRUD controllers and endpoints (not listed here).
- Add validation (FluentValidation or DataAnnotations) to API DTOs.
- Add unit/integration tests for services/repositories and client components.
- Wire client pages (`HomePage`, `TodoFormPage`) to API endpoints.
- Add error handling, loading states, and optimistic updates in the client.