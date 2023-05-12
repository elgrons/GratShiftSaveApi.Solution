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

      builder.Entity<GratShift>().HasData(
        new GratShift { GratShiftId = 1, CashTip = 100, CreditTip = 300, ShiftSales = 1800, ShiftDate = new DateTime(2023, 3, 1) },
        new GratShift { GratShiftId = 2, CashTip = 80, CreditTip = 400, ShiftSales = 1900, ShiftDate = new DateTime(2022, 12, 11) }

      );
    }
  }
}