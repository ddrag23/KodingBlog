using KodingBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KodingBlog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
    
        private readonly KodingBlogContext dbContext;
        public CategoryController(KodingBlogContext context) {
            dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await dbContext.Categories.ToListAsync();
            return Ok(data);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            var data = await dbContext.Categories.SingleOrDefaultAsync(i => i.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post(Category category)
        {

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();
            return Ok("Data berhasil disimpan");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Category categoryDto)
        {
            var category = await dbContext.Users.SingleOrDefaultAsync(i => i.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            dbContext.Categories.Update(categoryDto);
            await dbContext.SaveChangesAsync();
            return Ok("Data berhasil diupdate");

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await dbContext.Categories.SingleOrDefaultAsync(i => i.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            dbContext.Categories.Remove(data);
            await dbContext.SaveChangesAsync();
            return Ok("Data kategori berhasil dihapus");
        }
    }
}
