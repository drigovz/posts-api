using System.Threading.Tasks;
using PostsApi.Data;
using PostsApi.Repository.Categories;
using PostsApi.Repository.Comments;
using PostsApi.Repository.Posts;

namespace PostsApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private PostsRepository _postsRepository;
        private CategoriesRepository _categoriesRepository;
        private CommentsRepository _commentsRepository;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IPostsRepository PostsRepository
        {
            get
            {
                return _postsRepository = _postsRepository ?? new PostsRepository(_context);
            }
        }

        public ICategoriesRepository CategoriesRepository
        {
            get
            {
                return _categoriesRepository = _categoriesRepository ?? new CategoriesRepository(_context);
            }
        }

        public ICommentsRepository CommentsRepository
        {
            get
            {
                return _commentsRepository = _commentsRepository ?? new CommentsRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}