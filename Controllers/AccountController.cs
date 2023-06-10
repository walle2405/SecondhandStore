using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.Models;
using SecondhandStore.Services;

namespace SecondhandStore.Controllers
{
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

        // GET all action
        [HttpGet]
        public async Task<IActionResult> GetAccountList()
        {
            var accountList = await _accountService.GetAllAccounts();

            if (accountList.Count == 0 || !accountList.Any())
                return NotFound();

            return Ok(accountList);
        }

        // GET by Id action
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(string id)
        {   var existingAccount = await _accountService.GetAccountById(id);
            if (existingAccount == null) 
                return NotFound();
            return Ok(existingAccount);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateNewAccount(AccountCreateRequest accountCreateRequest)
        {
            var mappedAccount = _mapper.Map<Account>(accountCreateRequest);

            await _accountService.AddAccount(mappedAccount);

            return CreatedAtAction(nameof(GetAccountList),
                new { id = mappedAccount.AccountId },
                mappedAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(string id, AccountUpdateRequest accountUpdateRequest)
        {
            var mappedAccount = _mapper.Map<Account>(accountUpdateRequest);
            mappedAccount.AccountId = id;

            var existingAccount = await _accountService.GetAccountById(id);

            if (existingAccount.ToString() is null)
                return NotFound();

            await _accountService.UpdateAccount(mappedAccount);

            return NoContent();
        }


        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> DeleteRole(int id)
        //{
        //    var existingRole = await _roleService.GetRoleById(id);

        //    if (existingRole.ToString() is null)
        //        return NotFound();

        //    await _roleService.DeleteRole(existingRole);

        //    return NoContent();
        //}



        // POST action 
        //[HttpPost]
        //public IActionResult CreateNewAccount(Account account)
        //{
        //    AccountService.Create(account);
        //    return CreatedAtAction(nameof(Get), new { id = account.AccountId }, account);
        //}

        //// PUT action
        //[HttpPut("{id}")]
        //public IActionResult Update(string id, Account account)
        //{
        //    // This code will update the account and return a result
        //    if (id != account.AccountId)
        //        return BadRequest();

        //    var existingAccount = accountService.GetAll().Where(p => p.AccountId.Equals(id)).FirstOrDefault();
        //    if (existingAccount is null)
        //        return NotFound();

        //    accountService.Update(account);

        //    return NoContent();
        //}

        //// DELETE action
        //[HttpDelete("{id}")]
        //public IActionResult Delete(string id)
        //{
        //    // This code will delete the pizza and return a result
        //    var account = accountService.GetAll().Where(p => p.AccountId.Equals(id)).FirstOrDefault();

        //    if (account is null)
        //        return NotFound();

        //    accountService.Delete(account);

        //    return NoContent();
        //}
    }
}

