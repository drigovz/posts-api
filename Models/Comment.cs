using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostsApi.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The atribute text is required")]
        public string Text { get; set; }
        public DateTime? Date { get; set; } = DateTime.UtcNow;
    }
}