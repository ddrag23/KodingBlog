using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KodingBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        [HttpGet]
        public IActionResult index([FromQuery] string filePath)
        {
            try
            {
                bool checkFile = System.IO.File.Exists(filePath);
                if (checkFile)
                {

                    return Ok(filePath);
                }
                else
                {
                    throw new BadHttpRequestException("File not found");
                }
            }
            catch (HttpRequestException ex) when(ex.StatusCode == HttpStatusCode.BadRequest) 
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    var ext = Path.GetExtension(file.FileName);
                    String[] extensions = { ".png", ".jpg", ".jpeg", ".pdf" };
                    if (!Array.Exists(extensions, e => e == ext))
                        throw new Exception("File Not Allowed");

                    var path = Path.GetFullPath(Path.Combine(System.Environment.CurrentDirectory, "Storage", "tmp"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var filePath = Path.Combine(path, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return Ok(ext);
                }
                else
                {
                    throw new Exception("File Not Found");
                    ;
                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
        }
    }
}
