using TodoApi.Models;

namespace TodoApi.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(CreateUserDto createDto);
    Task<bool> UpdateAsync(int id, UpdateUserDto updateDto);
    Task<bool> DeleteAsync(int id);
}