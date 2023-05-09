using System.ComponentModel.DataAnnotations;

namespace GratShiftSaveApi.Models
{
  public class UserLogin
  {
    [Key]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
  }
}