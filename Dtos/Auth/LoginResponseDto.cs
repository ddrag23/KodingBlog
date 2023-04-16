
using KodingBlog.Dtos.User;

namespace KodingBlog.Dtos.Auth
{
    public class LoginResponseDto
    {
        public UserInfoDto User { get; set; }
        public String Token { get; set; }
    }
}
