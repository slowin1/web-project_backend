using EUseControl.DataAccess.Context;
using eUseControl.Domain.DTOs;
using eUseControl.Domain.Entities.Specialist;
using Microsoft.EntityFrameworkCore;

namespace eUseControl.BussinessLogic.Core.Specialists;

public class SpecialistActions
{
    private readonly UserContext _context;

    protected SpecialistActions(UserContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SpecialistResponseDto>> GetAllAsync()
    {
        var specialists = await _context.Specialists.AsNoTracking().ToListAsync();
        return specialists.Select(MapSpecialist);
    }

    public async Task<SpecialistResponseDto?> GetByIdAsync(string id)
    {
        var specialist = await _context.Specialists.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return specialist is null ? null : MapSpecialist(specialist);
    }

    public async Task<SpecialistResponseDto> CreateAsync(CreateSpecialistDto dto)
    {
        var specialist = new SpecialistData
        {
            Id = Guid.NewGuid().ToString(),
            FullName = dto.FullName.Trim(),
            PhoneNumber = dto.PhoneNumber.Trim(),
            Bio = dto.Bio.Trim(),
            PhotoUrl = dto.PhotoUrl.Trim(),
            IsActive = dto.IsActive
        };

        _context.Specialists.Add(specialist);
        await _context.SaveChangesAsync();

        return MapSpecialist(specialist);
    }

    public async Task<SpecialistResponseDto?> UpdateAsync(string id, UpdateSpecialistDto dto)
    {
        var specialist = await _context.Specialists.FindAsync(id);
        if (specialist is null)
        {
            return null;
        }

        specialist.FullName = dto.FullName.Trim();
        specialist.PhoneNumber = dto.PhoneNumber.Trim();
        specialist.Bio = dto.Bio.Trim();
        specialist.PhotoUrl = dto.PhotoUrl.Trim();
        specialist.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();
        return MapSpecialist(specialist);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var specialist = await _context.Specialists.FindAsync(id);
        if (specialist is null)
        {
            return false;
        }

        _context.Specialists.Remove(specialist);
        await _context.SaveChangesAsync();
        return true;
    }

    private static SpecialistResponseDto MapSpecialist(SpecialistData specialist)
    {
        return new SpecialistResponseDto
        {
            Id = specialist.Id,
            FullName = specialist.FullName,
            PhoneNumber = specialist.PhoneNumber,
            Bio = specialist.Bio,
            PhotoUrl = specialist.PhotoUrl,
            IsActive = specialist.IsActive
        };
    }
}
