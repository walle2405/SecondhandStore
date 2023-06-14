using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository
{
    public class PostRepository : BaseRepository<Post>
    {
        public PostRepository(SecondhandStoreContext dbContext) : base(dbContext)
        {
        }
    }
}
