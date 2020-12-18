using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostsApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The attribute title is required")]
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public int NumLikes { get; set; }
        public int Views { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}