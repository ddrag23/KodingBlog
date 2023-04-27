using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace KodingBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        [HttpGet]
        public  IActionResult Index([FromQuery] string filePath)
        {
            try
            {
                bool checkFile = System.IO.File.Exists(filePath);
                if (checkFile)
                {

                    var path = Path.GetFullPath(Path.Combine(System.Environment.CurrentDirectory, filePath));

                    FileStream stream = System.IO.File.Open(path,FileMode.Open);
                    return File(stream, GetContentType(path));
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            catch (HttpRequestException ex) when(ex.StatusCode == HttpStatusCode.NotFound) 
            {
                return NotFound(ex.Message);
            }
            
        }

        [NonAction]
        private static string GetContentType(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            switch (ext)
            {
                case ".png":
                    return "image/png";
                case ".jpg":
                    return "image/jpg";
                case ".jpeg":
                    return "image/jpeg";
                case ".pdf":
                    return "application/pdf";
                default:
                    return "application/octet-stream";
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
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
                    var fileName = Guid.NewGuid().ToString() + ext;
                    var filePath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return Ok(Path.Combine("Storage","tmp",fileName));
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
