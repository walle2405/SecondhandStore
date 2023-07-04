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
        public async Task<IEnumerable<ExchangeOrder>> GetExchangeRequest(int userId) {
            return await _dbContext.ExchangeOrders.Where(c => c.BuyerId == userId).Include(p => p.Seller).Include(p=>p.Post).Include(p => p.OrderStatus).ToListAsync();

        }
        public async Task<IEnumerable<ExchangeOrder>> GetExchangeOrder(int userId)
        {
            return await _dbContext.ExchangeOrders.Where(c => c.SellerId == userId).Include(p=>p.Buyer).Include(p => p.Post).Include(p=>p.OrderStatus).ToListAsync();
        }
    }
}
