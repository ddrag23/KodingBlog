using KodingBlog.Models;
using System.ComponentModel.DataAnnotations;

namespace KodingBlog.Dtos.Post
{
    public class CreateDto
    {
        [Required]
        public required int UserId { get; set; }
        [Required]
        public required int CategoryId { get; set; }
        public string? ImageContent { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Content { get; set; }
    }
}
