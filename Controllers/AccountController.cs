using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Models;
using SecondhandStore.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SecondhandStore.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly IMapper _mapper;

    public AccountController(AccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [SwaggerOperation(Summary =
        "Login for account with username and password. Return JWT Token if login successfully")]
    [HttpPost("login")]
    public async Task<IActionResult> LoginManagement([FromBody] LoginModelRequest loginModel)
    {
        // Tự tạo method login trong AccountService
        var loggedIn = await _accountService.Login(loginModel);
        
        if (loggedIn == null) 
            return BadRequest(new { message = "Invalid username or password." });
        
        var jwtToken = _accountService.CreateToken(loggedIn);
        return Ok(new
        {
            token = jwtToken
        });

    }

    // GET all action
    [HttpGet("get-account-list")]
    [Authorize(Roles = "AD")]
    public async Task<IActionResult> GetAccountList()
    {
        var accountList = await _accountService.GetAllAccounts();

        if (!accountList.Any())
            return NotFound();

        return Ok(accountList);
    }

    // GET by Id action
    [HttpGet("get-account-by-id")]
    [Authorize(Roles = "AD")]
    public async Task<IActionResult> GetAccountById(int id)
    {
        var existingAccount = await _accountService.GetAccountById(id);
        if (existingAccount is null)
            return NotFound();
        return Ok(existingAccount);
    }

    [HttpGet("get-user-by-name")]
    public async Task<IActionResult> GetUserByName(string fullName)
    {
        var existingUser = await _accountService.GetUserByName(fullName);
        if (existingUser is null)
            return NotFound();
        var mappedExistingUser = _mapper.Map<List<AccountEntityViewModel>>(existingUser);
        return Ok(mappedExistingUser);
        
    }

    [HttpPost("create-new-account")]
    public async Task<IActionResult> CreateNewAccount(AccountCreateRequest accountCreateRequest)
    {
        var mappedAccount = _mapper.Map<Account>(accountCreateRequest);

        await _accountService.AddAccount(mappedAccount);

        return CreatedAtAction(nameof(GetAccountList),
            new { id = mappedAccount.AccountId },
            mappedAccount);
    }

    [HttpPut("update-account")]
    [Authorize(Roles="US")]
    public async Task<IActionResult> UpdateAccount(AccountUpdateRequest accountUpdateRequest)
    {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId") ?.Value ?? string.Empty;
            
            var mappedAccount = _mapper.Map<Account>(accountUpdateRequest);  
            mappedAccount.AccountId = int.Parse(userId);

            await _accountService.UpdateAccount(mappedAccount);

            return NoContent();
    }


    [HttpPut("toggle-account-status")]
    [Authorize(Roles="AD")]
    public async Task<IActionResult> ToggleAccountStatus(int id)
    {
        try
        {
            var existingAccount = await _accountService.GetAccountById(id);

            if (existingAccount is null)
                return NotFound();
            
            await _accountService.ToggleAccountStatus(existingAccount);

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Invalid Request");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

        return Ok(new { message = "Logged out successfully." });
    }
    
}