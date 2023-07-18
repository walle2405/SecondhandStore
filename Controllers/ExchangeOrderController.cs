using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Models;
using SecondhandStore.Services;
using System.Data;
using Microsoft.AspNetCore.Mvc.Routing;
using SecondhandStore.Extension;
using System;

namespace SecondhandStore.Controllers
{

    [ApiController]
    public class ExchangeOrderController : ControllerBase
    {
        private readonly ExchangeOrderService _exchangeOrderService;
        private readonly PostService _postService;
        private readonly AccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public ExchangeOrderController(ExchangeOrderService exchangeOrderService,PostService postService,AccountService accountService, IServiceProvider serviceProvider, IMapper mapper)
        {
            _accountService = accountService;
            _postService = postService; 
            _exchangeOrderService = exchangeOrderService;
            _emailService = serviceProvider.GetRequiredService<IEmailService>();
            _mapper = mapper;
        }
        [HttpGet("get-all-exchange-for-admin")]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> GetAllExchangement() {
            var exchangeList = await _exchangeOrderService.GetAllExchange();
            if (exchangeList == null) {
                return NotFound("No exchange found");
            }
            var mappedExchange = exchangeList.Select(p => _mapper.Map<ExchangeViewEntityModel>(p));
            return Ok(mappedExchange);
        }

        // GET all action
        [HttpGet("get-all-request-list")]
        [Authorize(Roles = "US")]
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
        [Authorize(Roles = "US")]
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
            if (chosenPost.AccountId == parseUserId || chosenPost.PostStatusId == 2 || order.Any()) {
                return BadRequest("You cannot choose this post!");
            }
            var mappedExchange = _mapper.Map<ExchangeOrder>(exchangeOrderCreateRequest);
            mappedExchange.BuyerId = parseUserId;
            mappedExchange.SellerId = chosenPost.AccountId;
            mappedExchange.OrderDate = DateTime.Now;
            mappedExchange.OrderStatusId = 6;
            mappedExchange.PostId = chosenPost.PostId;
            var seller = await _accountService.GetAccountById(chosenPost.AccountId);
            var buyer = await _accountService.GetAccountById(parseUserId);
            try
            {
                SendMailModel request = new SendMailModel();
                request.ReceiveAddress = seller.Email;
                request.Subject = "New Order Notification";
                EmailContent content = new EmailContent();
                content.Dear = "Dear " + seller.Fullname + ",";
                content.BodyContent = "You have new order from " + buyer.Fullname + " for a product: " + chosenPost.ProductName + ".\nPlease check your exchange order.\nThank You!";
                request.Content = content.ToString(); 
                _emailService.SendMail(request);
            }
            catch (Exception ex)
            {
                return BadRequest("Cannot send email");
            }
            
            await _exchangeOrderService.AddExchangeRequest(mappedExchange);
            return Ok("Request Successfully");

        }
        [HttpPut("seller-accept-request")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> AcceptFromSeller(int orderId) {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            int parseUserId = Int32.Parse(userId);
            var exchange = await _exchangeOrderService.GetExchangeById(orderId);
            var relatedPostExchangeList = await _exchangeOrderService.GetExchangesListByPostId(exchange.PostId);
            if (exchange == null)
            {
                return BadRequest("Error!");
            }
            if (relatedPostExchangeList.Any()) {
                return BadRequest("Sorry, you have accepted a request!");
            }
            else
            {
                
                exchange.OrderStatusId = 4;
                await _exchangeOrderService.UpdateExchange(exchange);
                var chosenPost = await _postService.GetPostById(exchange.PostId);
                var seller = await _accountService.GetAccountById(exchange.SellerId);
                var buyer = await _accountService.GetAccountById(exchange.BuyerId);
                try
                {
                    SendMailModel request = new SendMailModel();
                    request.ReceiveAddress = buyer.Email;
                    request.Subject = "Accepted Order";
                    EmailContent content = new EmailContent();
                    content.Dear = "Dear " + buyer.Fullname + ",";
                    content.BodyContent = seller.Fullname + " have accepted a request with order Id #" + orderId + ":" + chosenPost.ProductName + ".\nPlease, check your exchange request for tracking progress." + "\nHave a nice day!";
                    request.Content = content.ToString();
                    _emailService.SendMail(request);
                }
                catch (Exception ex)
                {
                    return BadRequest("Cannot send email");
                }
                return Ok("Successful Confirmation.");
            }
        }
        [HttpPut("confirm-receive-from-buyer")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> ConfirmReceive(int orderId) {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            int parseUserId = Int32.Parse(userId);
            var exchange = await _exchangeOrderService.GetExchangeById(orderId);
            if (exchange == null)
            {
                return BadRequest("Error!");
            }
            else {
                if (exchange.OrderStatusId != 4)
                {
                    return BadRequest("You can't confirm until seller accept.");
                }
                else {
                    exchange.OrderStatusId = 8;
                    await _exchangeOrderService.UpdateExchange(exchange);
                    var relatedExchange = await _exchangeOrderService.GetAllRelatedProductPost(exchange.PostId, exchange.OrderId);
                    var chosenPost = await _postService.GetPostById(exchange.PostId);
                    chosenPost.PostStatusId = 2;
                    foreach (var exchangeComponent in relatedExchange)
                    {
                        exchangeComponent.OrderStatusId = 7;
                        await _exchangeOrderService.UpdateExchange(exchangeComponent);
                        var seller = await _accountService.GetAccountById(exchangeComponent.SellerId);
                        var buyer = await _accountService.GetAccountById(exchangeComponent.BuyerId);
                        try
                        {
                            SendMailModel request = new SendMailModel();
                            request.ReceiveAddress = buyer.Email;
                            request.Subject = "Cancel Order Notification";
                            EmailContent content = new EmailContent();
                            content.Dear = "Dear " + buyer.Fullname + ",";
                            content.BodyContent = seller.Fullname + " have cancelled a request with order Id #" + orderId + ":" + chosenPost.ProductName + ".\nReason: Another request for this product has been complete." + "\nHave a nice day!";
                            request.Content = content.ToString();
                            _emailService.SendMail(request);
                        }
                        catch (Exception ex)
                        {
                            return BadRequest("Cannot send email");
                        }
                    }
                    return Ok("Delivered Successfully! Thank you.");
                }
            }
        }
        [HttpPut("cancel-exchange")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> RejectReceive(int orderId)
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
                exchange.OrderStatusId = 7;
                await _exchangeOrderService.UpdateExchange(exchange);
                var seller = await _accountService.GetAccountById(exchange.SellerId);
                var buyer = await _accountService.GetAccountById(exchange.BuyerId);
                try
                {
                    SendMailModel request = new SendMailModel();
                    request.ReceiveAddress = seller.Email;
                    request.Subject = "Cancel Order Notification";
                    EmailContent content = new EmailContent();
                    content.Dear = "Dear " + seller.Fullname + ",";
                    content.BodyContent = buyer.Fullname + " have cancel a request with order Id #" + orderId + ":" + chosenPost.ProductName + ".\nPlease, check your exchange request for tracking progress." + "\nHave a nice day!";
                    request.Content = content.ToString();
                    _emailService.SendMail(request);
                }
                catch (Exception ex)
                {
                    return BadRequest("Cannot send email");
                }
                return Ok("Cancelled Successfully!");
            }
        }

    }
}
