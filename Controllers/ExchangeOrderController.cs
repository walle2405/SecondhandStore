using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
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
        [Route("api/[controller]/get-all-order-list")]
        public async Task<IActionResult> GetOrderList()
        {
            var orderList = await _exchangeOrderService.GetAllRequest();

            if (!orderList.Any())
                return NotFound();

            return Ok(orderList);
        }

        // GET by Id action
        [HttpGet]
        [Route("api/[controller]/get-order-by-id")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var existingOrder = await _exchangeOrderService.GetOrderById(id);
            if (existingOrder is null)
                return NotFound();
            return Ok(existingOrder);
        }

        [HttpPost]
        [Route("api/[controller]/add-order")]
        public async Task<IActionResult> CreateOrder(ExchangeOrderCreateRequest exchangeOrderCreateRequest)
        {
            var mappedOrder = _mapper.Map<ExchangeOrder>(exchangeOrderCreateRequest);

            await _exchangeOrderService.AddOrder(mappedOrder);

            return CreatedAtAction(nameof(GetOrderList),
                new { id = mappedOrder.OrderDetailId },
                mappedOrder);
        }
        [HttpPut]
        [Route("api/[controller]/update-order-status/{toggle}")]
        public async Task<IActionResult> ToggleOrderStatus(int id)
        {
            try
            {
                var existingOrder = await _exchangeOrderService.GetOrderById(id);

                if (existingOrder is null)
                    return NotFound();

                existingOrder.OrderStatus = !existingOrder.OrderStatus;

                await _exchangeOrderService.UpdateOrder(existingOrder);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Invalid Request");
            }

        }
    }
}
