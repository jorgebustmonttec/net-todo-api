using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoService
{
    Task<IEnumerable<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task<Todo> CreateAsync(CreateTodoDto CreateDto);
    Task<bool> UpdateAsync(int id, UpdateTodoDto updateDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ToggleAsync(int id);
}