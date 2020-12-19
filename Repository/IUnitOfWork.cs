using System.Threading.Tasks;
using PostsApi.Repository.Posts;

namespace PostsApi.Repository
{
    public interface IUnitOfWork
    {
        IPostsRepository PostsRepository { get; }

        Task Commit();
    }
}