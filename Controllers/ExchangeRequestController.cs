using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.Models;
using SecondhandStore.Services;

namespace SecondhandStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRequestController : ControllerBase
    {
        private readonly ExchangeRequestService _exchangeRequestService;
        private readonly IMapper _mapper;
        public ExchangeRequestController(ExchangeRequestService exchangeRequestService, IMapper mapper)
        {
            _exchangeRequestService = exchangeRequestService;
            _mapper = mapper;
        }

        // GET all action
        [HttpGet]
        public async Task<IActionResult> GetRequestList()
        {
            var requestList = await _exchangeRequestService.GetAllRequest();

            if (requestList.Count == 0 || !requestList.Any())
                return NotFound();

            return Ok(requestList);
        }

        // GET by Id action
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var existingRequest = await _exchangeRequestService.GetRequestById(id);
            if (existingRequest is null)
                return NotFound();
            return Ok(existingRequest);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTopUp(ExchangeRequestCreateRequest exchangeRequestCreateRequest)
        {
            var mappedRequest = _mapper.Map<ExchangeRequest>(exchangeRequestCreateRequest);

            await _exchangeRequestService.AddRequest(mappedRequest);

            return CreatedAtAction(nameof(GetRequestList),
                new { id = mappedRequest.RequestDetailId },
                mappedRequest);
        }
    }
}
