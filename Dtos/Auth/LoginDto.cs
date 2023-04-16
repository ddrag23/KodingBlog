using System.ComponentModel.DataAnnotations;

namespace KodingBlog.Dtos.Auth
{
    public class LoginDto
    {
        [Required]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }

    }
}
