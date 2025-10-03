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

        group.MapPost("/", async (Todo newTodo, TodoDbContext db) =>
        {
            db.Todos.Add(newTodo);
            await db.SaveChangesAsync();
            return Results.Created($"/api/todos{newTodo.Id}", newTodo);
        });

        group.MapDelete("/{id}", async (int id, TodoDbContext db) =>
        {
            var todo = await db.Todos.FindAsync(id);
            if (todo is null)
            {
                return Results.NotFound();
            }
            db.Todos.Remove(todo);
            await db.SaveChangesAsync();
            return Results.Ok(todo);
        });
    }
}
