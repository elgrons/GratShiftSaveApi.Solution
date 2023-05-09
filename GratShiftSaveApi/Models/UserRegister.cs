using System.ComponentModel.DataAnnotations;
using GratShiftSaveApi.Models;
using System.Collections.Generic;

namespace GratShiftSaveApi.Models
{
  public class UserRegister
  {
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
  }
}