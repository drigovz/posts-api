using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PostsApi.DTOs
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PostTagDTO> Posts { get; set; } = new Collection<PostTagDTO>();
    }
}