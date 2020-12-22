using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostsApi.Data;
using PostsApi.Models;
using PostsApi.Repository.Generic;

namespace PostsApi.Repository.Comments
{
    public class CommentsRepository : Repository<Comment>, ICommentsRepository
    {
        public CommentsRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Comment>> GetCommentsOfPost(int post)
        {
            return await Get().Where(c => c.PostId == post).ToListAsync();
        }
    }
}