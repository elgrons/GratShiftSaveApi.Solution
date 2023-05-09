using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GratShiftSaveApi.Models
{
  public class UserRole
  {
    // public int UserRoleId { get; set; }
    public const string Admin = "Admin";
    public const string User = "User";
    }
}