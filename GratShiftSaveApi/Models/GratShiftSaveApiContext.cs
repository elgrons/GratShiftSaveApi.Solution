using Microsoft.EntityFrameworkCore;

namespace GratShiftSaveApi.Models
{
  public class GratShiftSaveApiContext : DbContext
  {
    public DbSet<GratShift> GratShifts { get; set; }
    public DbSet<User> Users { get; set; }

    public GratShiftSaveApiContext(DbContextOptions<GratShiftSaveApiContext> options) : base(options)
    {
    }

protected override void
    OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<GratShift>().HasData(
        new GratShift { GratShiftId = 1, CashTip = 100, CreditTip = 300, ShiftSales = 1800, ShiftDate = new DateTime(2023, 3, 1) }
      );
    }
  }
}