using Microsoft.EntityFrameworkCore;
using eUseControl.Domain.Entities.User;

namespace EUseControl.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<UserData> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserData>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserName).IsRequired();
            entity.Property(e => e.Email).IsRequired();
        });
    }
}
