using SecondhandStore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;
namespace SecondhandStore.Repository
{
    public class ReviewRepository : BaseRepository<Review>
    {
        private readonly SecondhandStoreContext _dbContext;
        public ReviewRepository(SecondhandStoreContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Review>> GetAllReviewbyReviewedId(int reviewedId) { 
            return await _dbContext.Reviews.Where(c => c.ReviewedId == reviewedId).Include(p => p.Reviewed).Include(p=>p.Reviewer).ToListAsync();
        }
    }
}
