using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Models;
using SecondhandStore.Services;
using System.Data;

namespace SecondhandStore.Controllers;

[ApiController]
[Route("topup")]
public class TopUpController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly TopUpService _topupService;
    private readonly AccountService _accountService;
    private readonly PostService _postService;

    public TopUpController(TopUpService topUpService, IMapper mapper, AccountService accountService,
        PostService postService)
    {
        _topupService = topUpService;
        _mapper = mapper;
        _accountService = accountService;
        _postService = postService;
    }

    // GET all action
    [HttpGet("get-all-topup-order")]
    [Authorize(Roles = "AD")]
    public async Task<IActionResult> GetTopupList()
    {
        var topupList = await _topupService.GetAllTopUp();
        if (!topupList.Any())
            return NotFound();
        var mappedExistTopup = topupList.Select(p => _mapper.Map<TopUpEntityViewModel>(p));
        return Ok(mappedExistTopup);

    }

    [HttpPost("send-topup-order")]
    [Authorize(Roles = "US")]
    public async Task<IActionResult> CreateNewTopUp(TopUpCreateRequest topUpCreateRequest)
    {
        var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ??
                     string.Empty;
        if (topUpCreateRequest == null)
        {
            return NoContent();
        }
        else
        {
            if (topUpCreateRequest.TopUpPoint < 10)
            {
                return BadRequest("Minimum point must be 10 point.");
            }
            else
            {
                var mappedTopup = _mapper.Map<TopUp>(topUpCreateRequest);
                mappedTopup.AccountId = Int32.Parse(userId);
                mappedTopup.Price = mappedTopup.TopUpPoint * 1000;
                mappedTopup.TopUpDate = DateTime.Now;
                mappedTopup.TopupStatusId = 3;

                await _topupService.AddTopUp(mappedTopup);

                return CreatedAtAction(nameof(GetTopupList),
                    new { id = mappedTopup.OrderId },
                    mappedTopup);
            }
        }
    }

    // function de user tu xem top-up cua minh
    [HttpGet("user-history-transaction")]
    [Authorize(Roles = "US")]
    public async Task<IActionResult> GetTopUpByUserId()
    {
        var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ??
                     string.Empty;
        var id = Int32.Parse(userId);
        var existingTopup = await _topupService.GetTopUpByUserId(id);

        if (existingTopup is null)
            return NotFound();

        var mappedExistTopup = _mapper.Map<List<TopUpEntityViewModel>>(existingTopup);
        var userMappedExistTopUp = existingTopup.Select(p => _mapper.Map<TopUpEntityViewModel>(p));
        return Ok(userMappedExistTopUp);
    }

    [HttpGet("get-user-total-topuptransaction-value")]
    [Authorize(Roles = "US")]
    public async Task<IActionResult> ReturnTotalTopupTransactionOfUser()
    {
        var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ??
                     string.Empty;
        var id = Int32.Parse(userId);
        return Ok(await _topupService.GetTotalValueOfUserTransaction(id));
    }

    [HttpGet("get-total-revenue")]
    [Authorize(Roles = "AD")]
    public async Task<IActionResult> ReturnRevenue()
    {
        return Ok(await _topupService.GetTotalRevenue());
    }

    [HttpPut("cancel-topup")]
    [Authorize(Roles = "US")]
    public async Task<IActionResult> cancelTopUp(int id)
    {
        var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ??
                     string.Empty;
        var topup = await _topupService.GetTopUpById(id);
        if (topup is null)
        {
            return NotFound();
        }
        else if (topup.AccountId != Int32.Parse(userId))
        {
            return Unauthorized();
        }
        else if (topup.TopupStatusId != 3)
        {
            return BadRequest();
        }
        else
        {
            if (topup.TopupStatusId == 7)
            {
                return NoContent();
            }
            else
            {
                await _topupService.CancelTopup(topup);
                return NoContent();
            }
        }
    }

    [HttpPut("accept-topup")]
    [Authorize(Roles = "AD")]
    public async Task<IActionResult> acceptedTopUp(int id)
    {
        var topup = await _topupService.GetTopUpById(id);
        if (topup is null)
        {
            return NotFound();
        }
        else
        {
            if (topup.TopupStatusId == 8)
            {
                return NoContent();
            }
            else
            {
                await _topupService.AcceptTopup(topup);
                var account = await _accountService.GetAccountById(topup.AccountId);
                account.PointBalance += topup.TopUpPoint;
                await _accountService.UpdatePointAutomatic(account);
                return NoContent();
            }
        }
    }

    [HttpGet("search-topup-by-email")]
    [Authorize(Roles = "AD")]
    public async Task<IActionResult> GetTopUpByEmail(string searchString)
    {
        var existingTopup = await _topupService.GetTopUpByEmailorPhone(searchString);
        if (existingTopup is null)
            return NotFound();
        var mappedExistTopup = _mapper.Map<List<TopUpEntityViewModel>>(existingTopup);
        var userMappedExistTopUp = existingTopup.Select(p => _mapper.Map<TopUpEntityViewModel>(p));
        return Ok(userMappedExistTopUp);
    }
} 