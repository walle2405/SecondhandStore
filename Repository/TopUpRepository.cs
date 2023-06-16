using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository;

public class TopUpRepository : BaseRepository<TopUp>
{
    public TopUpRepository(SecondhandStoreContext dbContext) : base(dbContext)
    {
    }
}