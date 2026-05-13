using AutoMapper;
using EUseControl.DataAccess.Context;
using eUseControl.BussinessLogic.Interfaces;
using eUseControl.Domain.DTOs;
using eUseControl.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace eUseControl.BussinessLogic.Core;

public class UserService : IUserService
{
    private readonly UserContext _context;
    private readonly IMapper _mapper;

    public UserService(UserContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    public async Task<UserResponseDto?> GetByIdAsync(string id)
    {
        var user = await _context.Users.FindAsync(id);
        return user is null ? null : _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
    {
        var user = _mapper.Map<UserData>(dto);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<UserResponseDto?> UpdateAsync(string id, UpdateUserDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null) return null;

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.UserName = dto.UserName;
        user.Phone = dto.Phone;

        await _context.SaveChangesAsync();
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UserResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == dto.UserName && u.Password == dto.Password);
        return user is null ? null : _mapper.Map<UserResponseDto>(user);
    }
}