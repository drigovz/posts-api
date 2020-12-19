using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostsApi.Data;
using PostsApi.Models;
using PostsApi.Repository.Generic;

namespace PostsApi.Repository.Posts
{
    public class PostsRepository : Repository<Post>, IPostsRepository
    {
        public PostsRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Post>> GetPostsForCategory(int category)
        {
            return await Get().Where(p => p.CategoryId == category).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsForDate(DateTime date)
        {
            return await Get().Where(p => p.CreatedAt == date).ToListAsync();
        }
    }
}