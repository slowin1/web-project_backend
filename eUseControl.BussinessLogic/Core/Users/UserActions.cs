using EUseControl.DataAccess.Context;
using eUseControl.Domain.DTOs;
using eUseControl.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace eUseControl.BussinessLogic.Core.Users;

public class UserActions
{
    private readonly UserContext _context;

    protected UserActions(UserContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var users = await _context.Users.AsNoTracking().ToListAsync();
        return users.Select(MapUser);
    }

    public async Task<UserResponseDto?> GetByIdAsync(string id)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return user is null ? null : MapUser(user);
    }

    public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
    {
        var user = new UserData
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = dto.FirstName.Trim(),
            LastName = dto.LastName.Trim(),
            UserName = dto.UserName.Trim(),
            Email = dto.Email.Trim(),
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Phone = dto.Phone.Trim(),
            RegisteredOn = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return MapUser(user);
    }

    public async Task<UserResponseDto?> UpdateAsync(string id, UpdateUserDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return null;
        }

        user.FirstName = dto.FirstName.Trim();
        user.LastName = dto.LastName.Trim();
        user.UserName = dto.UserName.Trim();
        user.Phone = dto.Phone.Trim();

        await _context.SaveChangesAsync();
        return MapUser(user);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    private static UserResponseDto MapUser(UserData user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            Phone = user.Phone,
            RegisteredOn = user.RegisteredOn
        };
    }
}
