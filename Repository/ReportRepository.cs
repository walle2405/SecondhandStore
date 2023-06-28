using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository
{
    public class ReportRepository:BaseRepository<Report>
    {
        private readonly SecondhandStoreContext _dbContext;
        public ReportRepository(SecondhandStoreContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Report>> GetByUserId(string userId)
        {
            return await _dbContext.Reports.Where(c => c.ReporterId.Equals(userId)).ToListAsync();
        }
    }
}
