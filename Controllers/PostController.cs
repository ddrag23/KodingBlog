using KodingBlog.Dtos.Post;
using KodingBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KodingBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly KodingBlogContext dbContext;
        public PostController(KodingBlogContext context)
        {
            dbContext = context;
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetWithUser()
        {
            var userId = User.Claims.First(x => x.Type == "UserId");
            var data = await dbContext.Posts.Where(w => w.UserId == int.Parse(userId.Value)).ToListAsync();
            return Ok(data);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await dbContext.Posts.ToListAsync();
            return Ok(data);
        }

        // GET api/<UserController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            var data = await dbContext.Posts.SingleOrDefaultAsync(i => i.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST api/<UserController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(CreateDto post)
        {
            if (ModelState.IsValid)
            {
                var slug = new String(post.Title).Replace(" ", "-");
                dbContext.Posts.Add(new Models.Post { Title = post.Title, Content = post.Content, CategoryId = post.CategoryId, UserId = post.UserId, Slug = slug });
                await dbContext.SaveChangesAsync();
                return Ok("Data berhasil disimpan");
            }
            else
            {
                return BadRequest();
            }
            
        }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Post postDto)
        {
            var category = await dbContext.Users.SingleOrDefaultAsync(i => i.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            dbContext.Posts.Update(postDto);
            await dbContext.SaveChangesAsync();
            return Ok("Data berhasil diupdate");

        }

        // DELETE api/<UserController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await dbContext.Posts.SingleOrDefaultAsync(i => i.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            dbContext.Posts.Remove(data);
            await dbContext.SaveChangesAsync();
            return Ok("Data kategori berhasil dihapus");
        }
    }
}
