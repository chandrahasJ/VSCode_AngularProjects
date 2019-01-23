using System.ComponentModel.DataAnnotations;

namespace NS.API.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength( 8, 
                      MinimumLength = 4,
                      ErrorMessage = "Password must have length between 4 and 8")]
        public string Password { get; set; }
    }
}