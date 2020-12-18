using System;
using System.ComponentModel.DataAnnotations;

namespace PostsApi.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The attribute name is required")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}