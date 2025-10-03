namespace TodoApi.Models;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    public string? Description { get; set; }
    public int user { get; set; } = 1;
}