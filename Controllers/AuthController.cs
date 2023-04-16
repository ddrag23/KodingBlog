using KodingBlog.Dtos.Auth;
using KodingBlog.Dtos.User;
using KodingBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KodingBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly KodingBlogContext dbContext;

        public AuthController(IConfiguration configuration, KodingBlogContext dbContext)
        {
            _configuration = configuration;
            this.dbContext = dbContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await dbContext.Users.Include(i => i.Role).SingleOrDefaultAsync(i => i.Email == dto.Email);
            if (user == null)
            {
                return BadRequest("Email yang anda masukkan salah");
            }

            var pass = BCrypt.Net.BCrypt.Verify(dto.Password,user.Password);
            if (!pass)
            {
                return BadRequest("Password yang anda masukkan salah");
            }
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Name",user.Name),
                new Claim("Email",user.Email),
                new Claim(ClaimTypes.Role,user.Role.Name),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            var userInfo = new UserInfoDto { Id = user.Id,Email = user.Email, Name = user.Name, PhoneNumber = user.PhoneNumber, RoleName = user.Role.Name};
            var response = new LoginResponseDto { User = userInfo, Token = new JwtSecurityTokenHandler().WriteToken(token) };
            return Ok(response);
        }
    }
}
