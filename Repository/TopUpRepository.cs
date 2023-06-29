using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository;

public class TopUpRepository : BaseRepository<TopUp>
{
    private readonly SecondhandStoreContext _dbContext;
    public TopUpRepository(SecondhandStoreContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<TopUp>> GetUserId(int userId)
    {
        return await _dbContext.TopUps.Where(c => c.AccountId == userId).ToListAsync();
    }
}