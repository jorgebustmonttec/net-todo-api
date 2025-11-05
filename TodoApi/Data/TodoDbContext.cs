using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relationship: Todo -> User (many-to-one)
        modelBuilder.Entity<Todo>()
            .HasOne(t => t.User)
            .WithMany(u => u.Todos)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed dummy users (Id=1 covers existing rows)
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Default User", Age = 30, Email = "default@example.com" },
            new User { Id = 2, Name = "Alice Adams", Age = 28, Email = "alice@example.com" },
            new User { Id = 3, Name = "Bob Brown", Age = 35, Email = "bob@example.com" }
        );
    }
}