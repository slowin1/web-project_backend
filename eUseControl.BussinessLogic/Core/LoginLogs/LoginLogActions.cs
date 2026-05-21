using EUseControl.DataAccess.Context;
using eUseControl.Domain.DTOs;
using eUseControl.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace eUseControl.BussinessLogic.Core.LoginLogs;

public class LoginLogActions
{
    private readonly UserContext _context;

    protected LoginLogActions(UserContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LoginLogResponseDto>> GetAllAsync()
    {
        var logs = await _context.LoginLogs.AsNoTracking().ToListAsync();
        return logs.Select(MapLog);
    }

    public async Task<LoginLogResponseDto?> GetByIdAsync(string id)
    {
        var log = await _context.LoginLogs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return log is null ? null : MapLog(log);
    }

    public async Task<LoginLogResponseDto> CreateAsync(CreateLoginLogDto dto)
    {
        var log = new ULoginData
        {
            Id = Guid.NewGuid().ToString(),
            UserIp = dto.UserIp.Trim(),
            LoginIp = dto.LoginIp.Trim(),
            LoginDataTime = dto.LoginDataTime
        };

        _context.LoginLogs.Add(log);
        await _context.SaveChangesAsync();

        return MapLog(log);
    }

    public async Task<LoginLogResponseDto?> UpdateAsync(string id, UpdateLoginLogDto dto)
    {
        var log = await _context.LoginLogs.FindAsync(id);
        if (log is null)
        {
            return null;
        }

        log.UserIp = dto.UserIp.Trim();
        log.LoginIp = dto.LoginIp.Trim();
        log.LoginDataTime = dto.LoginDataTime;

        await _context.SaveChangesAsync();
        return MapLog(log);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var log = await _context.LoginLogs.FindAsync(id);
        if (log is null)
        {
            return false;
        }

        _context.LoginLogs.Remove(log);
        await _context.SaveChangesAsync();
        return true;
    }

    private static LoginLogResponseDto MapLog(ULoginData log)
    {
        return new LoginLogResponseDto
        {
            Id = log.Id,
            UserIp = log.UserIp,
            LoginIp = log.LoginIp,
            LoginDataTime = log.LoginDataTime
        };
    }
}
