using KodingBlog.Models;
using System.ComponentModel.DataAnnotations;

namespace KodingBlog.Dtos.User
{
    public class CreateUserDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(12)]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
