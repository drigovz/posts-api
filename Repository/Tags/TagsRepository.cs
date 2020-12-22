using PostsApi.Data;
using PostsApi.Models;
using PostsApi.Repository.Generic;

namespace PostsApi.Repository.Tags
{
    public class TagsRepository : Repository<Tag>, ITagsRepository
    {
        public TagsRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}