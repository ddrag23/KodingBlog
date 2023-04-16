using System.ComponentModel.DataAnnotations;

namespace KodingBlog.Dtos.User
{
    public class UpdateUserDto
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
        public string? PhoneNumber { get; set; }
    }
}
