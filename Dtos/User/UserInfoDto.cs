using KodingBlog.Models;

namespace KodingBlog.Dtos.User
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public String? RoleName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
