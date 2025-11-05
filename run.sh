#!/usr/bin/env bash
set -euo pipefail

ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$ROOT"

API_PORT="${API_PORT:-5131}"
API_URL="http://localhost:${API_PORT}"
API_WAIT_TIMEOUT_MS="${API_WAIT_TIMEOUT_MS:-30000}"

CLIENT_CMD=$(cat <<EOF
sh -lc '
echo "[CLIENT] waiting for API at ${API_URL}/healthz (timeout ${API_WAIT_TIMEOUT_MS}ms)..."
if npx --yes wait-on --timeout ${API_WAIT_TIMEOUT_MS} "${API_URL}/healthz"; then
  echo "[CLIENT] API is healthy"
else
  echo "[CLIENT] WARNING: API health check timed out, starting client anyway."
fi
cd react-todo-client
echo "[CLIENT] starting react dev server..."
npm run dev
'
EOF
)

npx concurrently \
  --names "API,CLIENT" \
  --prefix "[{name}]" \
  --prefix-colors "cyan.bold,magenta.bold" \
  --kill-others \
  "ASPNETCORE_URLS=${API_URL} cd TodoApi && dotnet run --project ./TodoApi.csproj" \
  "$CLIENT_CMD"