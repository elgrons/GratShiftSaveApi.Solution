using Microsoft.EntityFrameworkCore;

namespace GratShiftSaveApi.Models
{
  public class GratShiftSaveApiContext : DbContext
  {
    public DbSet<GratShift> GratShifts { get; set; }

    public GratShiftSaveApiContext(DbContextOptions<GratShiftSaveApiContext> options) : base(options)
    {
    }
  }
}