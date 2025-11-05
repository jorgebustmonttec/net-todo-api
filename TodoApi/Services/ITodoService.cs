using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoService
{
    Task<IEnumerable<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task<IEnumerable<Todo>> GetByUserIdAsync(int userId);
    Task<Todo> CreateAsync(CreateTodoDto createDto);
    Task<bool> UpdateAsync(int id, UpdateTodoDto updateDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ToggleAsync(int id);
}