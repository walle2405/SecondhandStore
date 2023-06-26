using Microsoft.EntityFrameworkCore;
using SecondhandStore.Models;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using AutoMapper;
using SecondhandStoreContext = SecondhandStore.Infrastructure.SecondhandStoreContext;
using SecondhandStore.Repository;

namespace SecondhandStore.Services;
public class AccountService
{
    private readonly AccountRepository _accountRepository;

    public AccountService(AccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<List<Account>> GetAllAccounts()
    {
        //.Include(p => p) all you need
        return await _accountRepository.GetAll().ToListAsync();
    }

    public async Task<Account?> GetAccountById(string id)
    {
        return await _accountRepository.GetById(id);
    }
    
    public async Task AddAccount(Account account)
    {
        await _accountRepository.Add(account);
    }

    public async Task UpdateAccount(Account account)
    {
        await _accountRepository.Update(account);
    }

    public async Task DeleteAccount(Account account)
    {
        await _accountRepository.Delete(account);
    }
}