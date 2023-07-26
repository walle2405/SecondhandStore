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
        return await _dbContext.TopUps.Where(c => c.AccountId == userId)
            .Include(p => p.Account).Include(p => p.TopupStatus).ToListAsync();
    }
    public async Task CancelTopUp(TopUp cancelTopUp)
    {
        var topup = await _dbContext.TopUps.FirstOrDefaultAsync(a => a.OrderId == cancelTopUp.OrderId);
        if (topup != null)
        {
            topup.TopupStatusId = 7;
        }
        await _dbContext.SaveChangesAsync();
    }
    public async Task AcceptTopup(TopUp acceptedTopup)
    {
        var topup = await _dbContext.TopUps.FirstOrDefaultAsync(a => a.OrderId == acceptedTopup.OrderId);
        if (topup != null)
        {
            topup.TopupStatusId = 8;
        }
        await _dbContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<TopUp>> GetTopUpbyEmailorPhone(string searchString)
    {
        return await _dbContext.TopUps.Where(c => c.Account.Email.Contains(searchString) || c.Account.PhoneNo.Contains(searchString)).Include(p => p.Account).Include(p => p.TopupStatus).ToListAsync();
    }

}