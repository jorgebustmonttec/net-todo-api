using System.Collections.Generic;

namespace TodoApi.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = "User";

    public int Age { get; set; }

    public string Email { get; set; } = string.Empty;

    // Navigation
    public ICollection<Todo> Todos { get; set; } = new List<Todo>();
}