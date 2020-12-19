using PostsApi.Data;
using PostsApi.Repository.Posts;

namespace PostsApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private PostsRepository _postsRepository;
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

        public void Commit()
        {
            _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}