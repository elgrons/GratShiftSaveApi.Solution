using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GratShiftSaveApi.Models
{
  public class GratShiftUser
  {
    public int GratShiftUserId { get; set; }
    public int GratShiftId { get; set; }
    public GratShift GratShift { get; set; }
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
    public List <GratShiftUser> JoinEntities { get; }
  } 
}