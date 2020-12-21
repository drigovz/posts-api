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
            return await Get().AsNoTracking().Where(p => p.CategoryId == category).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsForDate(DateTime date)
        {
            return await Get().AsNoTracking().Where(p => p.CreatedAt == date).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetMostCommentedsPosts()
        {
            return await Get().AsNoTracking().OrderBy(p => p.Comments.Count).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByTag(string tag)
        {
            return await Get().Where(p => p.Tags.Any(t => t.Name == tag)).ToListAsync();

            //var tags = Get().ToList().Select(p => p.Tags);

            // var posts = Get().Where(p => p.Tags.Any(t => t.Name == tag)).ToList();

            //var posts = Get().AsNoTracking().SelectMany(p => p.Tags.Where(t => t.Name.Equals(tag))).ToList();

            // var tags = Get().AsNoTracking().Select(p => p.Tags.Where(t => t.Name == tag)).ToList();
            // var posts = Get().AsNoTracking();
        }
    }
}