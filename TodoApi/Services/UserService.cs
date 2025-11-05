using AutoMapper;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto createDto)
    {
        var entity = _mapper.Map<User>(createDto);
        var created = await _userRepository.CreateAsync(entity);
        return _mapper.Map<UserDto>(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdateUserDto updateDto)
    {
        var existing = await _userRepository.GetByIdAsync(id);
        if (existing == null) return false;
        _mapper.Map(updateDto, existing);
        await _userRepository.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _userRepository.GetByIdAsync(id);
        if (existing == null) return false;
        await _userRepository.DeleteAsync(id);
        return true;
    }
}