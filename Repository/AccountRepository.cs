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

    public new async Task Update(Account updatedAccount)
    {
        var existingAccount = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == updatedAccount.AccountId);

         if (existingAccount != null)
         {
             existingAccount.Password = existingAccount.Password; 
             existingAccount.Fullname = updatedAccount.Fullname ?? existingAccount.Fullname;
             existingAccount.Address = updatedAccount.Address ?? existingAccount.Address;
             existingAccount.PhoneNo = updatedAccount.PhoneNo ?? existingAccount.PhoneNo;
             existingAccount.CredibilityPoint = updatedAccount.CredibilityPoint;
             await _dbContext.SaveChangesAsync();
         } 
    }

    public new async Task ToggleAccountStatus(Account updatedAccount)
    {
        var existingAccount = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == updatedAccount.AccountId);
        if (existingAccount != null)
        {
            existingAccount.IsActive = !existingAccount.IsActive;
        }

        if (existingAccount.IsActive == true)
        {
            existingAccount.RoleId = "US";
        }

        if (existingAccount.IsActive == false)
        {
            existingAccount.RoleId = "DE";
        }
        await _dbContext.SaveChangesAsync();
    }
    public new async Task UpdatePointAutomatic(Account topupAccount)
    {
        var existingAccount = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == topupAccount.AccountId);
        if (existingAccount != null)
        {
            existingAccount.PointBalance = topupAccount.PointBalance;
        }
        await _dbContext.SaveChangesAsync();
    }
}