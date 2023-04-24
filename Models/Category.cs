namespace KodingBlog.Models
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
