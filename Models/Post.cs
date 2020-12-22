using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostsApi.Models
{
    [Table("Posts")]
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The attribute title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The attribute subtitle is required")]
        public string Subtitle { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "The attribute image is required")]
        public string Image { get; set; }
        public bool? IsActive { get; set; } = true;
        [Required(ErrorMessage = "The attribute category is required")]
        public int NumLikes { get; set; } = 0;
        public int Views { get; set; } = 0;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}