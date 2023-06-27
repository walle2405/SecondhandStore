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

    // GET by Id action
    [HttpGet("{id}")]
    [Authorize(Roles = "US")]
    public async Task<IActionResult> GetTopUpById(int id)
    {
        var existingTopup = await _topupService.GetTopUpById(id);
        if (existingTopup is null)
            return NotFound();
        var mappedExistTopup = _mapper.Map<TopUpEntityViewModel>(existingTopup);
        return Ok(mappedExistTopup);
    }

    [HttpPost]
    [Authorize(Roles = "US")]
    public async Task<IActionResult> CreateNewTopUp(TopUpCreateRequest topUpCreateRequest)
    {
        var mappedTopup = _mapper.Map<TopUp>(topUpCreateRequest);

        await _topupService.AddTopUp(mappedTopup);

        return CreatedAtAction(nameof(GetTopupList),
            new { id = mappedTopup.OrderId },
            mappedTopup);

    }
    [HttpGet("get-revenue")]
    [Authorize(Roles = "AD")]
    public async Task<IActionResult> ReturnRevenue() {
        return Ok(await _topupService.GetTotalRevenue());
    }
}