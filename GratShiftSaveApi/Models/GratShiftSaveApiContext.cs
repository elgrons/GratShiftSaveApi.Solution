using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace GratShiftSaveApi.Models
{
  public class GratShiftSaveApiContext : IdentityDbContext<IdentityUser>
  {
    public DbSet<GratShift> GratShifts { get; set; }
    public DbSet<UserResponse> UserResponses { get; set; }
    public GratShiftSaveApiContext(DbContextOptions<GratShiftSaveApiContext> options) : base(options)
    {
    }

    protected override void
        OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<GratShift>()
          .HasKey(gs => gs.GratShiftId);

      builder.Entity<GratShift>()
          .Property(gs => gs.UserId)
          .IsRequired();
    }
  }
}