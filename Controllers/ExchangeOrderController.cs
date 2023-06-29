using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Models;
using SecondhandStore.Services;

namespace SecondhandStore.Controllers
{

    [ApiController]
    public class ExchangeOrderController : ControllerBase
    {
        private readonly ExchangeOrderService _exchangeOrderService;
        private readonly IMapper _mapper;
        public ExchangeOrderController(ExchangeOrderService exchangeOrderService, IMapper mapper)
        {
            _exchangeOrderService = exchangeOrderService;
            _mapper = mapper;
        }

        // GET all action
        [HttpGet]
        [Route("api/[controller]/get-all-order-list-buyer")]
        public async Task<IActionResult> GetOrderListByBuyerId(int userId)
        {
            var orderList = await _exchangeOrderService.GetExchangeByBuyerId(userId);
            if (orderList is null)
            {
                return NotFound();
            }
            var mappedExchangeOrder = _mapper.Map<List<ExchangeOrderEntityViewModel>>(orderList);
            return Ok(mappedExchangeOrder);
            
        }
    }
}
