using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PostsApi.Models;
using PostsApi.Repository.Generic;

namespace PostsApi.Repository.Posts
{
    public interface IPostsRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsForDate(DateTime date);
        Task<IEnumerable<Post>> GetPostsForCategory(int category);
    }
}