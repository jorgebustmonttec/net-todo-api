using AutoMapper;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public TodoService(ITodoRepository todoRepository, IUserRepository userRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _userRepository = userRepository;
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

    public async Task<IEnumerable<Todo>> GetByUserIdAsync(int userId)
    {
        return await _todoRepository.GetByUserIdAsync(userId);
    }

    public async Task<Todo> CreateAsync(CreateTodoDto createDto)
    {
        // Validate userId exists
        var user = await _userRepository.GetByIdAsync(createDto.UserId);
        if (user == null)
        {
            throw new ArgumentException($"User with ID {createDto.UserId} does not exist.");
        }

        var newTodo = _mapper.Map<Todo>(createDto);
        newTodo.IsComplete = false;
        return await _todoRepository.CreateAsync(newTodo);
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
        }
        return false;
    }

    public async Task<bool> ToggleAsync(int id)
    {
        var existingTodo = await _todoRepository.GetByIdAsync(id);
        if (existingTodo == null)
        {
            return false;
        }

        existingTodo.IsComplete = !existingTodo.IsComplete;
        await _todoRepository.UpdateAsync(existingTodo);
        return true;
    }

}