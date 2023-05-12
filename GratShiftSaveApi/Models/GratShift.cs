using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;

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
    public int UserId { get; set; }
  }
}