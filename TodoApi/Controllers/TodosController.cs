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
    /// Retrieves a list of all Todo items.
    /// </summary>
    /// <returns>A list of Todo items.</returns>
    /// <response code="200">Returns the list of items.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodos()
    {
        var todos = await _todoService.GetAllAsync();
        return Ok(todos);
    }

    /// <summary>
    /// Retrieves a specific Todo item by its unique ID.
    /// </summary>
    /// <param name="id" example="1">The ID of the Todo item to retrieve.</param>
    /// <returns>The requested Todo item.</returns>
    /// <response code="200">Returns the requested item.</response>
    /// <response code="404">If the item with the specified ID is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// Creates a new Todo item.
    /// </summary>
    /// <param name="createDto">The data for the new Todo item.</param>
    /// <returns>The newly created Todo item.</returns>
    /// <response code="201">Returns the newly created item and a location header.</response>
    /// <response code="400">If the provided data is invalid.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Todo>> CreateTodo(CreateTodoDto createDto)
    {
        var createdTodo = await _todoService.CreateAsync(createDto);

        return CreatedAtAction(
            nameof(GetTodoById),
            new { id = createdTodo.Id },
            createdTodo);
    }
    /// <summary>
    /// Updates an existing Todo item.
    /// </summary>
    /// <param name="id" example="1">The ID of the Todo item to update.</param>
    /// <param name="updateDto">The updated data for the Todo item.</param>
    /// <returns></returns>
    /// <response code="204">If the update was successful.</response>
    /// <response code="404">If the item with the specified ID is not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTodo(int id, UpdateTodoDto updateDto)
    {
        var wasUpdated = await _todoService.UpdateAsync(id, updateDto);
        if (wasUpdated == false)
        {
            return NotFound();
        }
        return NoContent();
    }

    /// <summary>
    /// Deletes a specific Todo item.
    /// </summary>
    /// <param name="id" example="1">The ID of the Todo item to delete.</param>
    /// <returns></returns>
    /// <response code="204">If the deletion was successful.</response>
    /// <response code="404">If the item with the specified ID is not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var wasDeleted = await _todoService.DeleteAsync(id);
        if (wasDeleted == false)
        {
            return NotFound();
        }
        return NoContent();
    }
}