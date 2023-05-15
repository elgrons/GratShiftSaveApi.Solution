using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string UserId { get; set; }
  }
}