using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecondhandStore.EntityRequest;
using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository;
using System.Linq;
namespace SecondhandStore.Services;

public class AccountService
{
    private readonly AccountRepository _accountRepository;
    private readonly IConfiguration _configuration;

    public AccountService(AccountRepository accountRepository, IConfiguration configuration)
    {
        _accountRepository = accountRepository;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Account>> GetAllAccounts()
    {
        //.Include(p => p) all you need
        return await _accountRepository.GetAll().ToListAsync();
    }

    public async Task<Account?> GetAccountById(int id)
    {
        return await _accountRepository.GetByIntId(id);
    }

    public async Task<IEnumerable<Account>> GetUserByName(string name)
    {
        return await _accountRepository.GetUserByName(name);
    }

    public async Task<Account?> Login(LoginModelRequest loginModelRequest)
    {
        return await _accountRepository.Login(loginModelRequest);
    }
    
    public async Task AddAccount(Account account)
    {
        await _accountRepository.Add(account);
    }

    public async Task UpdateAccount(Account account)
    {
        await _accountRepository.Update(account);
    }
    
    public async Task ToggleAccountStatus (Account account)
    {
        await _accountRepository.ToggleAccountStatus(account);
    }
    public async Task DeleteAccount(Account account)
    {
        await _accountRepository.Delete(account);
    }
    public async Task UpdatePointAutomatic(Account topupAccount) { 
        await _accountRepository.UpdatePointAutomatic(topupAccount);
    }

    public string CreateToken(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, account.RoleId),
            new("accountId", account.AccountId.ToString()),
            new (ClaimTypes.Name, account.Fullname),
            new (ClaimTypes.Email, account.Email)
        };
        
        
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JwtToken:NotTokenKeyForSureSourceTrustMeDude"]));

        var credential = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            _configuration["JwtToken:Issuer"],
            _configuration["JwtToken:Audience"],
            claims,
            expires: DateTime.UtcNow.AddDays(21),
            signingCredentials: credential);

        return tokenHandler.WriteToken(token);
    }
    
    
}