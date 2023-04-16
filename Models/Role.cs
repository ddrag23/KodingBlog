namespace KodingBlog.Models
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public List<User>? users { get; set; }
    }

}
