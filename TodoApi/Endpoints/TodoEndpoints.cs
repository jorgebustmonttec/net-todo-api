using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Endpoints;

public static class TodoEndpoints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/todos");

        group.MapGet("/", async (TodoDbContext db) =>
        {
            return await db.Todos.ToListAsync();
        });
    }
}
