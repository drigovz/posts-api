using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostsApi.Models
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The attribute name is required")]
        public string Name { get; set; }
        public bool? IsActive { get; set; } = true;
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Post> Posts { get; set; } = new Collection<Post>();
    }
}