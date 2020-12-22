using System.Threading.Tasks;
using PostsApi.Repository.Categories;
using PostsApi.Repository.Comments;
using PostsApi.Repository.Images;
using PostsApi.Repository.Posts;
using PostsApi.Repository.Tags;

namespace PostsApi.Repository
{
    public interface IUnitOfWork
    {
        IPostsRepository PostsRepository { get; }
        ICategoriesRepository CategoriesRepository { get; }
        ICommentsRepository CommentsRepository { get; }
        ITagsRepository TagsRepository { get; }
        IImageRepository ImagesRepository { get; }

        Task Commit();
    }
}