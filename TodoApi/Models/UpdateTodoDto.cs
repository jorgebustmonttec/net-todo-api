namespace TodoApi.Models;

public class updateTodoDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsComplete { get; set; }
}