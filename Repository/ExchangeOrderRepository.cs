using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository
{
    public class ExchangeOrderRepository:BaseRepository<ExchangeOrder>
    {   
        private readonly  SecondhandStoreContext _dbContext;
        public ExchangeOrderRepository(SecondhandStoreContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ExchangeOrder>> GetExchangeByBuyerId(int userId) {
            return await _dbContext.ExchangeOrders.Where(c => c.BuyerId == userId).ToListAsync();

        }
        public async Task<IEnumerable<ExchangeOrder>> GetExchangeBySellerId(int userId)
        {
            return await _dbContext.ExchangeOrders.Where(c => c.SellerId == userId).ToListAsync();
        }
    }
}
