using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }


    /// <summary>
    /// Returns a list of all todo items.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodos()
    {
        var todos = await _todoService.GetAllAsync();
        return Ok(todos);
    }

    /// <summary>
    ///  Retrieves a specific todo item by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetTodoById(int id)
    {
        var todo = await _todoService.GetByIdAsync(id);
        if (todo == null)
        {
            return NotFound();
        }
        return Ok(todo);
    }


    /// <summary>
    /// Post endpoint to create a todo item with a provided todo in JSON with title and description only.
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Todo>> CreateTodo(CreateTodoDto createDto)
    {
        var createdTodo = await _todoService.CreateAsync(createDto);

        return CreatedAtAction(
            nameof(GetTodoById),
            new { id = createdTodo.Id },
            createdTodo);
    }

    /// <summary>
    /// Deletes a todo item with the provided id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        // HINT: The controller's job is just to call the service.
        // Call the _todoService.DeleteAsync(id) method. It returns a boolean.
        var wasDeleted = await _todoService.DeleteAsync(id);

        // HINT: Now, check the result.
        // If 'wasDeleted' is false, it means the service couldn't find the item.
        // What should you return? (Look at the GetTodoById method for a clue).
        if (wasDeleted == false)
        {
            return NotFound();
        }
        return NoContent();

        // HINT: If 'wasDeleted' is true, the delete was successful.
        // The standard HTTP response for a successful DELETE is "204 No Content".
        // The helper method for this is `return NoContent();`
    }

    /// <summary>
    /// Update a todo item.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="todo"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, Todo todo)
    {
        var todoUpdated = await _todoService.UpdateAsync(id, todo);
        if (todoUpdated == false)
        {
            return NotFound();
        }
        return Ok(todo);
    }
}