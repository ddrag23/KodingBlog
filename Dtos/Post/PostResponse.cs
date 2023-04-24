using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;

namespace KodingBlog.Dtos.Post
{
    public record PostResponse(int Id, int UserId, string Title,string Content, string CategoryName,int CategoryId, string Slug)
    {
    }
}
