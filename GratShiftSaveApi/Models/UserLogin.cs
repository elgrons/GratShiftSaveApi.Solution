using System.ComponentModel.DataAnnotations;

namespace GratShiftSaveApi.Models
{
  public class UserLogin
  {
    [Required(ErrorMessage = "User Name is required")]
    [Key]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
  }
}