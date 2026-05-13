using eUseControl.Domain.DTOs;

namespace eUseControl.BussinessLogic.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto?> GetByIdAsync(string id);
    Task<UserResponseDto> CreateAsync(CreateUserDto dto);
    Task<UserResponseDto?> UpdateAsync(string id, UpdateUserDto dto);
    Task<bool> DeleteAsync(string id);
    Task<UserResponseDto?> LoginAsync(LoginDto dto);
}