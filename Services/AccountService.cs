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

    public async Task<List<Account>> GetAllAccounts()
    {
        return await _accountRepository.GetAll();
    }

    public async Task<Account?> GetAccountById(string id)
    {
        return await _accountRepository.GetById(id);
    }

    public async Task<bool> Login(LoginModelRequest loginModelRequest)
    {
        using (var _context = new SecondhandStoreContext())
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(u => u.Email.Equals(loginModelRequest.Email)
                                                             && u.Password.Equals(loginModelRequest.Password));
            if (account == null)
            {
                // User not found in database
                return false;
            }
            // Username and password are correct
            return true;
        }
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

    public string CreateToken(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, account.RoleId),
            new(ClaimTypes.Email, account.Email),
            new("accountId", account.AccountId)
        };
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JwtToken:NotTokenKeyForSureSourceTrustMeDude"]));

        var credential = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            _configuration["JwtToken:Issuer"],
            _configuration["JwtToken:Audience"],
            claims,
            expires: DateTime.UtcNow.AddDays(21),
            signingCredentials: credential);

        return tokenHandler.WriteToken(token);
    }
}