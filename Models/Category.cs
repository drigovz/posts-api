using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostsApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The attribute name is required")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}