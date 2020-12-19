using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostsApi.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The attribute name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}