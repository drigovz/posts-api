using System.ComponentModel.DataAnnotations;

namespace PostsApi.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "The attribute URL is required")]
        public string Url { get; set; }
    }
}