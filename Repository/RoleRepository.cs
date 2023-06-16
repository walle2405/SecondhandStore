using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository;

public class RoleRepository : BaseRepository<Role>
{
    public RoleRepository(SecondhandStoreContext dbContext) : base(dbContext)
    {
    }
}