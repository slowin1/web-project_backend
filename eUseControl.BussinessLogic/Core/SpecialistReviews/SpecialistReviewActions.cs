using EUseControl.DataAccess.Context;
using eUseControl.Domain.DTOs;
using eUseControl.Domain.Entities.Specialist;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities.services;
using eUseControl.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace eUseControl.BussinessLogic.Core.SpecialistReviews;

public class SpecialistReviewActions
{
    private readonly UserContext _context;

    protected SpecialistReviewActions(UserContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SpecialistReviewResponseDto>> GetAllAsync()
    {
        var reviews = await _context.Reviews
            .AsNoTracking()
            .Include(review => review.Booking)
            .Include(review => review.Client)
            .OrderByDescending(review => review.CreatedAt)
            .ToListAsync();

        return reviews.Select(MapReview);
    }

    public async Task<IEnumerable<SpecialistReviewResponseDto>> GetBySpecialistAsync(string specialistId)
    {
        var reviews = await _context.Reviews
            .AsNoTracking()
            .Include(review => review.Booking)
            .Include(review => review.Client)
            .Where(review => review.Booking != null && review.Booking.SpecialistId == specialistId)
            .OrderByDescending(review => review.CreatedAt)
            .ToListAsync();

        return reviews.Select(MapReview);
    }

    public async Task<SpecialistReviewResponseDto?> GetByIdAsync(string id)
    {
        var review = await _context.Reviews
            .AsNoTracking()
            .Include(item => item.Booking)
            .Include(item => item.Client)
            .FirstOrDefaultAsync(x => x.Id == id);

        return review is null ? null : MapReview(review);
    }

    public async Task<SpecialistReviewResponseDto> CreateAsync(CreateSpecialistReviewDto dto)
    {
        var booking = await GetEligibleBookingAsync(dto.ClientId, dto.BookingId, dto.SpecialistId);
        var hasReview = await _context.Reviews
            .AsNoTracking()
            .AnyAsync(review => review.ClientId == dto.ClientId.Trim() && review.BookingId == booking.Id);

        if (hasReview)
        {
            throw new InvalidOperationException("Вы уже оставили отзыв по этой записи.");
        }

        var review = new SpecialistReview
        {
            Id = Guid.NewGuid().ToString(),
            Rating = dto.Rating,
            Comment = dto.Comment.Trim(),
            CreatedAt = DateTime.UtcNow,
            ClientId = dto.ClientId.Trim(),
            BookingId = booking.Id
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        review.Booking = booking;
        review.Client = await _context.Users.AsNoTracking().FirstAsync(user => user.Id == review.ClientId);
        return MapReview(review);
    }

    public async Task<SpecialistReviewResponseDto?> UpdateAsync(string id, UpdateSpecialistReviewDto dto)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review is null)
        {
            return null;
        }

        var booking = await GetEligibleBookingAsync(dto.ClientId, dto.BookingId, dto.SpecialistId);

        review.Rating = dto.Rating;
        review.Comment = dto.Comment.Trim();
        review.ClientId = dto.ClientId.Trim();
        review.BookingId = booking.Id;

        await _context.SaveChangesAsync();

        review.Booking = booking;
        review.Client = await _context.Users.AsNoTracking().FirstAsync(user => user.Id == review.ClientId);
        return MapReview(review);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review is null)
        {
            return false;
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<ServiceBookingData> GetEligibleBookingAsync(string clientId, string bookingId, string? specialistId)
    {
        var normalizedClientId = clientId.Trim();
        var normalizedBookingId = bookingId.Trim();
        var normalizedSpecialistId = string.IsNullOrWhiteSpace(specialistId)
            ? null
            : specialistId.Trim();

        if (string.IsNullOrWhiteSpace(normalizedClientId) || string.IsNullOrWhiteSpace(normalizedBookingId))
        {
            throw new InvalidOperationException("Не указан клиент или запись для отзыва.");
        }

        var clientExists = await _context.Users.AnyAsync(user => user.Id == normalizedClientId);
        if (!clientExists)
        {
            throw new InvalidOperationException("Клиент не найден.");
        }

        var booking = await _context.Bookings
            .AsNoTracking()
            .FirstOrDefaultAsync(item =>
                item.Id == normalizedBookingId ||
                item.BookingId == normalizedBookingId);

        if (booking is null)
        {
            throw new InvalidOperationException("Запись для отзыва не найдена.");
        }

        if (booking.ClientUserId != normalizedClientId)
        {
            throw new InvalidOperationException("Отзыв может оставить только клиент этой записи.");
        }

        if (booking.Status != BookingStatus.Completed)
        {
            throw new InvalidOperationException("Отзыв можно оставить только после выполненной услуги.");
        }

        if (!string.IsNullOrWhiteSpace(normalizedSpecialistId) && booking.SpecialistId != normalizedSpecialistId)
        {
            throw new InvalidOperationException("Эта запись относится к другому специалисту.");
        }

        if (string.IsNullOrWhiteSpace(booking.SpecialistId))
        {
            throw new InvalidOperationException("У записи не указан специалист.");
        }

        return booking;
    }

    private static SpecialistReviewResponseDto MapReview(SpecialistReview review)
    {
        return new SpecialistReviewResponseDto
        {
            Id = review.Id,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt,
            ClientId = review.ClientId,
            BookingId = review.BookingId,
            SpecialistId = review.Booking?.SpecialistId,
            UserName = GetUserName(review.Client)
        };
    }

    private static string? GetUserName(UserData? user)
    {
        if (user is null)
        {
            return null;
        }

        var fullName = $"{user.FirstName} {user.LastName}".Trim();
        return string.IsNullOrWhiteSpace(fullName) ? user.UserName : fullName;
    }
}
