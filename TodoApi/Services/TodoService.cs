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

        public async Task<Todo?> GetByIdAsync(int id)
    {
        return await _todoRepository.GetByIdAsync(id);
    }
    public Task<Todo> CreateAsync(Todo newTodo)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Todo>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(int id, Todo todo)
    {
        throw new NotImplementedException();
    }
}