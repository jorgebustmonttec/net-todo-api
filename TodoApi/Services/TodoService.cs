using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
        return await _todoRepository.GetAllAsync();
    }

    public async Task<Todo?> GetByIdAsync(int id)
    {
        return await _todoRepository.GetByIdAsync(id);
    }
    public Task<Todo> CreateAsync(CreateTodoDto createDto)
    {
        var newTodo = new Todo
        {
            Title = createDto.Title,
            Description = createDto.Description,
            IsComplete = false,
            user = 1
        };
        return _todoRepository.CreateAsync(newTodo);
    }

    public async Task<bool> UpdateAsync(int id, Todo todo)
    {
        var existingTodo = await _todoRepository.GetByIdAsync(id);
        if (existingTodo == null)
        {
            return false;
        }
        existingTodo.Title = todo.Title;
        existingTodo.IsComplete = todo.IsComplete;
        existingTodo.Description = todo.Description;

        await _todoRepository.UpdateAsync(existingTodo);

        return true;

    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingTodo = await _todoRepository.GetByIdAsync(id);
        if (existingTodo != null)
        {
            await _todoRepository.DeleteAsync(id);
            return true;
        } return false;
    }
}