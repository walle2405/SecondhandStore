using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository
{
    public class ReviewRepository : BaseRepository<Review>
    {
        public ReviewRepository(SecondhandStoreContext dbContext) : base(dbContext)
        {
        }
    }
}
