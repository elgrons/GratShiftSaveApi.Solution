using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GratShiftSaveApi.Models
{
  public static class UserRole
  {
    public const string Admin = "Admin";
    public const string User = "User";
    }
}