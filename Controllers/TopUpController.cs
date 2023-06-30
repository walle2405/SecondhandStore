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

    public TopUpController(TopUpService topUpService, IMapper mapper)
    {
        _topupService = topUpService;
        _mapper = mapper;
    }

    // GET all action
    [HttpGet]
    [Authorize(Roles = "AD")]
    public async Task<IActionResult> GetTopupList()
    {
        var topupList = await _topupService.GetAllTopUp();

        if (!topupList.Any())
            return NotFound();
        var mappedExistTopup = _mapper.Map<List<TopUpEntityViewModel>>(topupList);
        return Ok(mappedExistTopup);
        
    }
    [HttpPost]
    [Authorize(Roles = "US")]
    public async Task<IActionResult> CreateNewTopUp(TopUpCreateRequest topUpCreateRequest)
    {
        var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId") ?.Value ?? string.Empty;
       
        var mappedTopup = _mapper.Map<TopUp>(topUpCreateRequest);
        mappedTopup.AccountId = Int32.Parse(userId);
        mappedTopup.Price = mappedTopup.TopUpPoint * 1000;
       
        mappedTopup.TopUpDate = DateTime.Now;
      
        await _topupService.AddTopUp(mappedTopup);

        return CreatedAtAction(nameof(GetTopupList),
            new { id = mappedTopup.OrderId },
            mappedTopup);

    }
    
    // function de user tu xem top-up cua minh
    [HttpGet("get-topup-by-userId")]
    [Authorize(Roles = "US")]
    public async Task<IActionResult> GetTopUpByUserId(int id)
    {
        var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId") ?.Value ?? string.Empty;
        
        var existingTopup = await _topupService.GetTopUpByUserId(id);
      
        if (existingTopup is null)
            return NotFound();
        
        var mappedExistTopup = _mapper.Map<List<TopUpEntityViewModel>>(existingTopup);
        var userMappedExistTopUp = mappedExistTopup.Where(c => c.AccountId == Int32.Parse(userId)).ToList();
        return Ok(userMappedExistTopUp);
    }

    [HttpGet("get-revenue")]
    [Authorize(Roles = "AD")]
    public async Task<IActionResult> ReturnRevenue() {
        return Ok(await _topupService.GetTotalRevenue());
    }
}