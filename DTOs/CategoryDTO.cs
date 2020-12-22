using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PostsApi.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<PostDTO> Posts { get; set; } = new Collection<PostDTO>();
    }
}