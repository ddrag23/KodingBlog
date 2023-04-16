using KodingBlog.Dtos.User;
using KodingBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KodingBlog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private KodingBlogContext dbContext;
        public UserController(KodingBlogContext dbContext) {
            this.dbContext = dbContext;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var users = await dbContext.Users.Include(i => i.Role).ToListAsync();
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(i => i.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateUserDto dto)
        {
            var user = new User();
            user.Email = dto.Email!;
            user.Name = dto.Name!;
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password!);
            user.RoleId = dto.RoleId;
            user.PhoneNumber = dto.PhoneNumber;
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return Ok("Data berhasil disimpan");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateUserDto dto)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(i => i.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.Email = dto.Email!;
            user.Name = dto.Name!;
            user.RoleId = dto.RoleId;
            user.PhoneNumber = dto.PhoneNumber;
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return Ok("Data berhasil diupdate");

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(i => i.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return Ok("Data user berhasil dihapus");
        }
    }
}
