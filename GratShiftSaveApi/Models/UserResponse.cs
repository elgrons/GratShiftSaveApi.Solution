using System.ComponentModel.DataAnnotations;

namespace GratShiftSaveApi.Models
{
  public class UserResponse
  {
        [Key]
        public string Status { get; set; }
        public string Message { get; set; }
    }
}