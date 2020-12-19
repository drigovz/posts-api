using System.Collections.Generic;
using PostsApi.Models;

namespace PostsApi.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Post> Posts { get; set; }
    }
}