namespace TodoApi.Models;

/// <summary>
/// The data transfer object for creating a new Todo item.
/// </summary>
public class CreateTodoDto
{
    /// <summary>
    /// The title of the new task. This field is required.
    /// </summary>
    /// <example>Learn ASP.NET Core</example>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// An optional description for the new task.
    /// </summary>
    /// <example>Follow the official Microsoft documentation and build a sample project.</example>
    public string? Description { get; set; }
}