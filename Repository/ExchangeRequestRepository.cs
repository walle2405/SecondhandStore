using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository
{
    public class ExchangeRequestRepository : BaseRepository<ExchangeRequest>
    {
        public ExchangeRequestRepository(SecondhandStoreContext dbContext):base(dbContext)
        {
            
        }
    }
}
