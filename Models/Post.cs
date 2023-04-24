namespace KodingBlog.Models
{
    public class Post
    {
        public int Id { get; set; }

        public required int UserId { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public User? User { get; set; }
        public string? ImageContent { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Content { get; set; }

    }
}
