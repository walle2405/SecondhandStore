using Microsoft.EntityFrameworkCore;
using SecondhandStore.EntityRequest;
using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository;

public class AccountRepository : BaseRepository<Account>
{
    private readonly SecondhandStoreContext _dbContext;

    public AccountRepository(SecondhandStoreContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    // Custom method implementation
    public async Task<Account?> Login(LoginModelRequest loginModelRequest)
    {
        return await _dbContext.Accounts
            .FirstOrDefaultAsync(a
                => a.Email == loginModelRequest.Email 
                   && a.Password == loginModelRequest.Password);
    }
}