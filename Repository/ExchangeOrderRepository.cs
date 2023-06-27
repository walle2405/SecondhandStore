using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository
{
    public class ExchangeOrderRepository:BaseRepository<ExchangeOrder>
    {
        public ExchangeOrderRepository(SecondhandStoreContext dbContext) : base(dbContext)
        {

        }
    }
}
