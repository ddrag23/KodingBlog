using KodingBlog.Dtos.User;
using KodingBlog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KodingBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private KodingBlogContext dbContext;
        public RoleController(KodingBlogContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var roles = await dbContext.Roles.ToListAsync();
            return Ok(roles);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            var role = await dbContext.Roles.SingleOrDefaultAsync(i => i.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post(Role role)
        {
           
            dbContext.Roles.Add(role);
            await dbContext.SaveChangesAsync();
            return Ok("Data berhasil disimpan");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Role roleDto)
        {
            var role = await dbContext.Users.SingleOrDefaultAsync(i => i.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            
            dbContext.Roles.Update(roleDto);
            await dbContext.SaveChangesAsync();
            return Ok("Data berhasil diupdate");

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await dbContext.Users.SingleOrDefaultAsync(i => i.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            dbContext.Users.Remove(role);
            await dbContext.SaveChangesAsync();
            return Ok("Data user berhasil dihapus");
        }
    }
}
