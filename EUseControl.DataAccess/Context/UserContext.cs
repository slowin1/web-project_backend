using Microsoft.EntityFrameworkCore;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities.Specialist;
using eUseControl.Domain.Entities.services;

namespace EUseControl.DataAccess.Context;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) { }

    public DbSet<UserData> Users { get; set; }
    public DbSet<ULoginData> LoginLogs { get; set; }
    public DbSet<SpecialistData> Specialists { get; set; }
    public DbSet<SpecialistWorkSchedule> WorkSchedules { get; set; }
    public DbSet<SpecialistReview> Reviews { get; set; }
    public DbSet<ServiceData> Services { get; set; }
    public DbSet<ServiceCategoryData> ServiceCategories { get; set; }
    public DbSet<ServiceImgData> ServiceImages { get; set; }
    public DbSet<ServiceTimeSlot> TimeSlots { get; set; }
    public DbSet<ServiceBookingData> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ServiceData -> Category
        modelBuilder.Entity<ServiceData>()
            .HasOne(s => s.Category)
            .WithMany()
            .HasForeignKey(s => s.CategoryId);

        // ServiceTimeSlot -> ServiceBookingData
        modelBuilder.Entity<ServiceTimeSlot>()
            .HasOne(t => t.Booking)
            .WithMany()
            .HasForeignKey(t => t.BookingId)
            .IsRequired(false);

        // ServiceImgData -> ServiceData
        modelBuilder.Entity<ServiceImgData>()
            .HasOne(i => i.Service)
            .WithMany()
            .HasForeignKey(i => i.ServiceId);

        // SpecialistWorkSchedule -> SpecialistData
        modelBuilder.Entity<SpecialistWorkSchedule>()
            .HasOne(s => s.SpecialistData)
            .WithMany()
            .HasForeignKey(s => s.SpecialistId);

        // SpecialistReview -> UserData
        modelBuilder.Entity<SpecialistReview>()
            .HasOne(r => r.Client)
            .WithMany()
            .HasForeignKey(r => r.ClientId);

        // SpecialistReview -> ServiceBookingData
        modelBuilder.Entity<SpecialistReview>()
            .HasOne(r => r.Booking)
            .WithMany()
            .HasForeignKey(r => r.BookingId)
            .IsRequired(false);

        // SpecialistData -> Services (many-to-many)
        modelBuilder.Entity<SpecialistData>()
            .HasMany(s => s.Services)
            .WithMany();

        // SpecialistData -> TimeSlots
        modelBuilder.Entity<SpecialistData>()
            .HasMany(s => s.TimeSlots)
            .WithOne()
            .HasForeignKey(t => t.SpecialistId);
    }
}
