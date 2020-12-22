using System.Collections.Generic;
using System.Threading.Tasks;
using PostsApi.Models;
using PostsApi.Repository.Generic;

namespace PostsApi.Repository.Comments
{
    public interface ICommentsRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsOfPost(int post);
    }
}