using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostsApi.Models
{
    [Table("Images")]
    public class Image
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "The attribute URL is required")]
        public string Url { get; set; }
    }
}