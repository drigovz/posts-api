namespace PostsApi.DTOs
{
    public class PostTagDTO
    {
        public int PostId { get; set; }
        public PostDTO Posts { get; set; }
        public int TagId { get; set; }
        public TagDTO Tags { get; set; }
    }
}