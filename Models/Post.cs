using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<PostTag> Tags { get; set; } = new Collection<PostTag>();
        public ICollection<Image> Images { get; set; } = new Collection<Image>();
        public ICollection<Comment> Comments { get; set; } = new Collection<Comment>();
    }
}