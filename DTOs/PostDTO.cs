using System.Collections.Generic;
using PostsApi.Models;

namespace PostsApi.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; } = true;
        public int CategoryId { get; set; }
        public int NumLikes { get; set; } = 0;
        public int Views { get; set; } = 0;
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}