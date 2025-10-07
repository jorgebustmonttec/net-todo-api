using TodoApi.Models;

namespace TodoApi.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetAllSync();
    Task<Todo?> GetByIdAsync(int id);
    Task<Todo> CreateAsync(Todo todo);
    Task UpdateAsync(Todo todo);
    Task DeleteAsync(int id);
}
