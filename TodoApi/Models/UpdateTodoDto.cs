namespace TodoApi.Models;

/// <summary>
/// The data transfer object for updating an existing Todo item.
/// </summary>
public class UpdateTodoDto
{
    /// <summary>
    /// The new title of the task.
    /// </summary>
    /// <example>Master ASP.NET Core Web APIs</example>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The new optional description for the task.
    /// </summary>
    /// <example>Refactor the project to use Clean Architecture and add unit tests.</example>
    public string? Description { get; set; }

    /// <summary>
    /// The new completion status of the task.
    /// </summary>
    /// <example>true</example>
    public bool IsComplete { get; set; }

    /// <summary>
    /// The ID of the user this task is assigned to.
    /// </summary>
    /// <example>1</example>
    public int UserId { get; set; }
}