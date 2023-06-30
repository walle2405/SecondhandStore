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
        [Route("api/[controller]/get-all-request-list")]
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
        [HttpGet]
        [Route("api/[controller]/get-all-order-list")]
        public async Task<IActionResult> GetOrderListBySellerId(int userId)
        {
            var orderList = await _exchangeOrderService.GetExchangeBySellerId(userId);
            if (orderList is null)
            {
                return NotFound();
            }
            var mappedExchangeOrder = _mapper.Map<List<ExchangeRequestEntityViewModel>>(orderList);
            return Ok(mappedExchangeOrder);

        }
    }
}
