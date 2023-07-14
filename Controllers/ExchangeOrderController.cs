using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Models;
using SecondhandStore.Services;
using System.Data;

namespace SecondhandStore.Controllers
{

    [ApiController]
    public class ExchangeOrderController : ControllerBase
    {
        private readonly ExchangeOrderService _exchangeOrderService;
        private readonly PostService _postService;
        private readonly IMapper _mapper;
        public ExchangeOrderController(ExchangeOrderService exchangeOrderService,PostService postService, IMapper mapper)
        {
            _postService = postService; 
            _exchangeOrderService = exchangeOrderService;
            _mapper = mapper;
        }

        // GET all action
        [HttpGet("get-all-request-list")]
        [Authorize(Roles = "AD,US")]
        public async Task<IActionResult> GetExchangeRequest()
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            var id = Int32.Parse(userId);
            var requestList = await _exchangeOrderService.GetExchangeRequest(id);
            if (requestList is null)
            {
                return NotFound();
            }
            var mappedExchangeRequest = requestList.Select(p => _mapper.Map<ExchangeRequestEntityViewModel>(p));
            return Ok(mappedExchangeRequest);

        }
        [HttpGet("get-all-order-list")]
        [Authorize(Roles = "AD,US")]
        public async Task<IActionResult> GetExchangeOrder()
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            var id = Int32.Parse(userId);
            var orderList = await _exchangeOrderService.GetExchangeOrder(id);
            if (orderList is null)
            {
                return NotFound();
            }
            var mappedExchangeOrder = orderList.Select(p => _mapper.Map<ExchangeOrderEntityViewModel>(p));
            return Ok(mappedExchangeOrder);

        }
        [HttpPost("send-exchange-request")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> SendExchangeRequest(ExchangeOrderCreateRequest exchangeOrderCreateRequest) 
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            int parseUserId = Int32.Parse(userId);
            var chosenPost = await _postService.GetPostById(exchangeOrderCreateRequest.PostId);
            var order = await _exchangeOrderService.GetExchangeByPostId(parseUserId, chosenPost.PostId);
            if (chosenPost is null)
            {
                return NotFound();
            }
            if (chosenPost.AccountId == parseUserId || chosenPost.PostStatusId == 7 || order.Any()) {
                return BadRequest("You cannot choose this post!");
            }
            var mappedExchange = _mapper.Map<ExchangeOrder>(exchangeOrderCreateRequest);
            mappedExchange.BuyerId = parseUserId;
            mappedExchange.SellerId = chosenPost.AccountId;
            mappedExchange.OrderDate = DateTime.Now;
            mappedExchange.OrderStatusId = 5;
            mappedExchange.PostId = chosenPost.PostId;
            chosenPost.PostStatusId = 7;
            await _exchangeOrderService.AddExchangeRequest(mappedExchange);
            return Ok("Request Successfully");

        }
        [HttpPut("complete-request")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> AcceptExchangeRequest(int orderId) {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            int parseUserId = Int32.Parse(userId);
            var exchange = await _exchangeOrderService.GetExchangeById(orderId);
            if (exchange == null)
            {
                return BadRequest("Error!");
            }
            else {
                exchange.OrderStatusId = 6;
                await _exchangeOrderService.UpdateExchange(exchange);
                return Ok("Thank you, Your exchange has been completed !");
            }
        }
        [HttpPut("cancel-request")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> RejectExchangeRequest(int orderId)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            int parseUserId = Int32.Parse(userId);
            var exchange = await _exchangeOrderService.GetExchangeById(orderId);
            if (exchange == null)
            {
                return BadRequest("Error!");
            }
            else
            {
                var chosenPost = await _postService.GetPostById(exchange.PostId);
                exchange.OrderStatusId = 4;
                chosenPost.PostStatusId = 3;
                await _exchangeOrderService.UpdateExchange(exchange);
                return Ok("Cancelled Successfully!");
            }
        }

    }
}
