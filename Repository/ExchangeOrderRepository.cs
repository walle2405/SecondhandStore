using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository
{
    public class ExchangeOrderRepository : BaseRepository<ExchangeOrder>
    {
        private readonly SecondhandStoreContext _dbContext;
        public ExchangeOrderRepository(SecondhandStoreContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ExchangeOrder>> GetExchangeRequest(int userId)
        {
            return await _dbContext.ExchangeOrders.Where(c => c.BuyerId == userId).Include(p => p.Seller).Include(p => p.Post).Include(p => p.OrderStatus).ToListAsync();

        }
        public async Task<IEnumerable<ExchangeOrder>> GetExchangeOrder(int userId)
        {
            return await _dbContext.ExchangeOrders.Where(c => c.SellerId == userId).Include(p => p.Buyer).Include(p => p.Post).Include(p => p.OrderStatus).ToListAsync();
        }
        public async Task<IEnumerable<ExchangeOrder>> GetAllExchange()
        {
            return await _dbContext.ExchangeOrders.Include(p => p.Seller).Include(p => p.Buyer).Include(p => p.Post).Include(p => p.OrderStatus).ToListAsync();
        }
        public async Task<IEnumerable<ExchangeOrder>> GetAllRelatedProductPost(int postId, int orderId)
        {
            return await _dbContext.ExchangeOrders.Where(p => p.OrderId != orderId && p.PostId == postId).ToListAsync();
        }
        public async Task<IEnumerable<ExchangeOrder>> GetExchangeListByPostId(int postId)
        {
            return await _dbContext.ExchangeOrders.Where(p => p.PostId == postId && p.OrderStatusId == 6).ToListAsync();
        }
        public async Task<IEnumerable<Post>> GetPurchasedPost(int userId)
        {
            return await _dbContext.ExchangeOrders.Where(c => c.BuyerId == userId && c.OrderStatusId == 8).Include(p => p.Post).Select(p => p.Post).ToListAsync();
        }
    }
}
