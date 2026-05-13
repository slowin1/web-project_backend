using EUseControl.DataAccess.Context;
using eUseControl.Domain.DTOs;
using eUseControl.Domain.Entities.services;
using Microsoft.EntityFrameworkCore;

namespace eUseControl.BussinessLogic.Core.ServiceBookings;

public class ServiceBookingActions
{
    private readonly UserContext _context;

    protected ServiceBookingActions(UserContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServiceBookingResponseDto>> GetAllAsync()
    {
        var bookings = await _context.Bookings.AsNoTracking().ToListAsync();
        return bookings.Select(MapBooking);
    }

    public async Task<ServiceBookingResponseDto?> GetByIdAsync(string id)
    {
        var booking = await _context.Bookings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return booking is null ? null : MapBooking(booking);
    }

    public async Task<ServiceBookingResponseDto> CreateAsync(CreateServiceBookingDto dto)
    {
        var booking = new ServiceBookingData
        {
            Id = Guid.NewGuid().ToString(),
            BookingId = dto.BookingId.Trim(),
            BookingName = dto.BookingName.Trim(),
            BookingDescription = dto.BookingDescription.Trim(),
            BookingTime = dto.BookingTime,
            BookingDate = dto.BookingDate
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return MapBooking(booking);
    }

    public async Task<ServiceBookingResponseDto?> UpdateAsync(string id, UpdateServiceBookingDto dto)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking is null)
        {
            return null;
        }

        booking.BookingId = dto.BookingId.Trim();
        booking.BookingName = dto.BookingName.Trim();
        booking.BookingDescription = dto.BookingDescription.Trim();
        booking.BookingTime = dto.BookingTime;
        booking.BookingDate = dto.BookingDate;

        await _context.SaveChangesAsync();
        return MapBooking(booking);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking is null)
        {
            return false;
        }

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
        return true;
    }

    private static ServiceBookingResponseDto MapBooking(ServiceBookingData booking)
    {
        return new ServiceBookingResponseDto
        {
            Id = booking.Id,
            BookingId = booking.BookingId,
            BookingName = booking.BookingName,
            BookingDescription = booking.BookingDescription,
            BookingTime = booking.BookingTime,
            BookingDate = booking.BookingDate
        };
    }
}
