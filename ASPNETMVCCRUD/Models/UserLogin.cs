using System.ComponentModel.DataAnnotations;

namespace ASPNETMVCCRUD.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Name cannot be blank")]
        [MaxLength(50, ErrorMessage = "Do not enter more than 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Make sure you enter a password")]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}
