using System.ComponentModel.DataAnnotations;
using GratShiftSaveApi.Models;
using System.Collections.Generic;

namespace GratShiftSaveApi.Models
{
  public class GratShift
  {
    public int GratShiftId { get; set; }
    [Required]
    public int CashTip { get; set; }
    [Required]
    public int CreditTip { get; set; }
    [Required]
    public int ShiftSales { get; set; }
    [Required]
    public DateTime ShiftDate { get; set; }
  }
}