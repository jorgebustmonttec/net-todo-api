using AutoMapper;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public TodoService(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
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
        var newTodo = _mapper.Map<Todo>(createDto);

        newTodo.IsComplete = false;
        newTodo.user = 1;

        return _todoRepository.CreateAsync(newTodo);
    }

    public async Task<bool> UpdateAsync(int id, UpdateTodoDto updateDto)
    {
        var existingTodo = await _todoRepository.GetByIdAsync(id);
        if (existingTodo == null)
        {
            return false;
        }

        _mapper.Map(updateDto, existingTodo);

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