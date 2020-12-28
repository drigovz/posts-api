using PostsApi.Data;
using PostsApi.Models;
using PostsApi.Repository.Generic;

namespace PostsApi.Repository.PostsTags
{
    public class PostTagRepository : Repository<PostTag>, IPostTagRepository
    {
        public PostTagRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}