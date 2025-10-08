namespace TodoApi.Models;

public class CreateTodoDto
{
    public string TItle { get; set; } = string.Empty;
    public string? Description { get; set; }
}