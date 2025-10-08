using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoService
{
    Task<IEnumerable<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task<Todo> CreateAsync(CreateTodoDto CreateDto);
    Task<bool> UpdateAsync(int id, updateTodoDto updateDto);
    Task<bool> DeleteAsync(int id);
}