using Microsoft.EntityFrameworkCore;
using SecondhandStore.EntityRequest;
using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;
using SecondhandStore.Services;

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

    public async Task<IEnumerable<Account>> GetUserByName(string Fullname)
    {
        return await _dbContext.Accounts.Where(c => c.Fullname.ToLower().Contains(Fullname.ToLower()) && c.RoleId.Equals("US")).ToListAsync();
    }

    // public async Task UpdateAccount(Account updatedAccount)
    // {
    //     var existingAccount = _dbContext.Accounts.Find(updatedAccount.AccountId);
    //     if (existingAccount != null)
    //     {
    //         existingAccount.Password = updatedAccount.Password;
    //         existingAccount.Fullname = updatedAccount.Fullname;
    //         existingAccount.Address = updatedAccount.Address;
    //         existingAccount.PhoneNo = updatedAccount.PhoneNo;
    //         await _dbContext.SaveChangesAsync();
    //     }
    // }
}