using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository;

public class AccountRepository : BaseRepository<Account>
{
    public AccountRepository(SecondhandStoreContext dbContext) : base(dbContext)
    {
    }
}