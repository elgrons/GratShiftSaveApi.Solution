using System.ComponentModel.DataAnnotations;
using GratShiftSaveApi.Models;
using System.Collections.Generic;

namespace ParksApi.Models
{
  public class Grat
  {
    public int GratId { get; set; }
    [Required]
    public int CashTip { get; set; }
    [Required]
    public int CreditTip { get; set; }
    [Required]
    public int ShiftSales { get; set; }
    [Required]
    public DateTime ShiftDate { get; set; }
    [Required]
    [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10.")]
    public int Rating { get; set; }
  }
}