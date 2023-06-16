using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.Models;
using SecondhandStore.Services;

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
    public async Task<IActionResult> GetTopupList()
    {
        var topupList = await _topupService.GetAllTopUp();

        if (topupList.Count == 0 || !topupList.Any())
            return NotFound();

        return Ok(topupList);
    }

    // GET by Id action
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTopUpById(int id)
    {
        var existingTopup = await _topupService.GetTopUpById(id);
        if (existingTopup is null)
            return NotFound();
        return Ok(existingTopup);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewTopUp(TopUpCreateRequest topUpCreateRequest)
    {
        var mappedTopup = _mapper.Map<TopUp>(topUpCreateRequest);

        await _topupService.AddTopUp(mappedTopup);

        return CreatedAtAction(nameof(GetTopupList),
            new { id = mappedTopup.OrderId },
            mappedTopup);
    }
}