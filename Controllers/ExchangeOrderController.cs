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
        public ExchangeOrderController(ExchangeOrderService exchangeOrderService, PostService postService, AccountService accountService, IServiceProvider serviceProvider, IMapper mapper)
        {
            _accountService = accountService;
            _postService = postService;
            _exchangeOrderService = exchangeOrderService;
            _emailService = serviceProvider.GetRequiredService<IEmailService>();
            _mapper = mapper;
        }

        [HttpGet("get-all-exchange-for-admin")]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> GetAllExchangement()
        {
            var exchangeList = await _exchangeOrderService.GetAllExchange();
            if (exchangeList == null)
            {
                return NotFound("No exchange found");
            }
            var mappedExchange = exchangeList.Select(p => _mapper.Map<ExchangeViewEntityModel>(p));
            return Ok(mappedExchange);
        }
        // GET all buyer purchased post
        [HttpGet("get-purchased-post")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> GetPurchasedPost()
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            var id = Int32.Parse(userId);
            var requestList = await _exchangeOrderService.GetPurchasedPost(id);
            if (requestList is null)
            {
                return NotFound();
            }
            var mappedPostList = requestList.Select(p => _mapper.Map<PostEntityViewModel>(p));
            return Ok(mappedPostList);
        }

        // GET all requests list of buyer
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
        //get all orders list of seller
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

        //buyer send request for seller for exchange
        [HttpPost("buyer-send-exchange-request")]
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
            // seller cannot order their own post || post status is inactive       || order for a product already existed, cannot reorder once cancelled
            if (chosenPost.AccountId == parseUserId || chosenPost.PostStatusId == 2 || order.Any()) {
                return BadRequest("You cannot choose this post!");
            }
            var mappedExchange = _mapper.Map<ExchangeOrder>(exchangeOrderCreateRequest);
            mappedExchange.BuyerId = parseUserId;
            mappedExchange.SellerId = chosenPost.AccountId;
            mappedExchange.OrderDate = DateTime.Now;
            mappedExchange.OrderStatusId = 3;
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
                content.BodyContent = "You have new order from " + buyer.Fullname + " for a product: " + chosenPost.ProductName + ".\nPlease check your exchange order.\nThank you!";
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
        //seller accept a request and switch to processing status
        [HttpPut("seller-accept-request")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> AcceptFromSeller(int orderId)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            int parseUserId = Int32.Parse(userId);
            var exchange = await _exchangeOrderService.GetExchangeById(orderId);
            //select exchange order have the same post id and have processing status
            var samePostExchangeList = await _exchangeOrderService.GetExchangesListByPostId(exchange.PostId);
            if (exchange == null)
            {
                return BadRequest("Error!");
            }
            //if a processing order existed in exchange order, cannot accept other order with the same post
            if (samePostExchangeList.Any()) {
                return BadRequest("Sorry, you have accepted another request!");
            }
            if (exchange.OrderStatusId == 7) {
                return BadRequest("This order has been cancelled. Can't be accept");
            }
            if (exchange.OrderStatusId == 8) {
                return BadRequest("This order has been complete. Can't be accept");
            }
            else
            {
                //order is processing
                exchange.OrderStatusId = 6;
                await _exchangeOrderService.UpdateExchange(exchange);
                //identify post in order by postid
                var chosenPost = await _postService.GetPostById(exchange.PostId);
                //switch post status to inactive
                chosenPost.PostStatusId = 5;
                await _postService.UpdatePost(chosenPost);
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
                //confirmation successfull
                return Ok("Successful Confirmation, deliver to buyer please.");
            }
        }
        //seller cancel exchange
        [HttpPut("seller-cancel-exchange")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> SellerCancelOrder(int orderId)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            int parseUserId = Int32.Parse(userId);
            var exchange = await _exchangeOrderService.GetExchangeById(orderId);
            if (exchange == null)
            {
                return BadRequest("Error! Exchange not found");
            }
            if (exchange.OrderStatusId == 8)
            {
                return BadRequest("This exchange has been completed, you can't cancel.");
            }
            else
            {
                //Identify post in order by postId
                var chosenPost = await _postService.GetPostById(exchange.PostId);
                //switch order status to cancel
                exchange.OrderStatusId = 7;
                await _exchangeOrderService.UpdateExchange(exchange);
                //switch post status to active again
                chosenPost.PostStatusId = 4;
                await _postService.UpdatePost(chosenPost);
                var seller = await _accountService.GetAccountById(exchange.SellerId);
                var buyer = await _accountService.GetAccountById(exchange.BuyerId);
                try
                {
                    SendMailModel request = new SendMailModel();
                    request.ReceiveAddress = buyer.Email;
                    request.Subject = "Cancel Order Notification";
                    EmailContent content = new EmailContent();
                    content.Dear = "Dear " + buyer.Fullname + ",";
                    content.BodyContent = seller.Fullname + " have cancel a request with order Id #" + orderId + ":" + chosenPost.ProductName + ".\nPlease, check your exchange request for tracking progress." + "\nHave a nice day!";
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
        //buyer-cancel-exchange
        [HttpPut("buyer-cancel-exchange")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> BuyerCancelOrder(int orderId)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            int parseUserId = Int32.Parse(userId);
            var exchange = await _exchangeOrderService.GetExchangeById(orderId);
            if (exchange == null)
            {
                return BadRequest("Error! Exchange not found");
            }
            // if exchange is completed, cannot cancel
            if (exchange.OrderStatusId == 8) {
                return BadRequest("This exchange has been completed, you can't cancel.");
            }
            else
            {
                //Identify post in order by postId
                var chosenPost = await _postService.GetPostById(exchange.PostId);
                //switch order status to cancel
                exchange.OrderStatusId = 7;
                await _exchangeOrderService.UpdateExchange(exchange);
                //switch post status to active again
                chosenPost.PostStatusId = 4;
                await _postService.UpdatePost(chosenPost);
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

        //seller or buyer confirm that an exchange is completed
        [HttpPut("confirm-finished")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> ConfirmReceive(int orderId)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            int parseUserId = Int32.Parse(userId);
            var exchange = await _exchangeOrderService.GetExchangeById(orderId);
            var chosenPost = await _postService.GetPostById(exchange.PostId);
            if (exchange == null)
            {
                return BadRequest("Error!");
            }
            else
            {
                //check if order is being processed or not, if not, cannot hit complete button.
                if (exchange.OrderStatusId == 3)
                {
                    return BadRequest("Your order hasn't been delivered yet.");
                }
                //check if order is cancell or not, if yes, cannot hit complete button.
                if (exchange.OrderStatusId == 7) {
                    return BadRequest("This order has been cancelled. You can't complete.");
                }
                else
                {
                    //switch order status to complete
                    exchange.OrderStatusId = 8;
                    await _exchangeOrderService.UpdateExchange(exchange);
                    //choosing other orders with same post but different input orderId
                    var relatedExchange = await _exchangeOrderService.GetAllRelatedProductPost(exchange.PostId, exchange.OrderId);
                    if (relatedExchange == null)
                    {
                        return BadRequest("Invalid");
                    }
                    var seller = await _accountService.GetAccountById(exchange.SellerId);
                    var completeBuyer = await _accountService.GetAccountById(exchange.BuyerId);
                    chosenPost.PostStatusId = 8;
                    await _postService.UpdatePost(chosenPost);
                    //cancel remaining orders with the same post
                    foreach (var exchangeComponent in relatedExchange)
                    {
                        exchangeComponent.OrderStatusId = 7;
                        await _exchangeOrderService.UpdateExchange(exchangeComponent);
                        var buyer = await _accountService.GetAccountById(exchangeComponent.BuyerId);
                        //send email with the reason of cancellation
                        try
                        {
                            SendMailModel request = new SendMailModel();
                            request.ReceiveAddress = buyer.Email;
                            request.Subject = "Cancel Order Notification";
                            EmailContent content = new EmailContent();
                            content.Dear = "Dear " + buyer.Fullname + ",";
                            content.BodyContent = seller.Fullname + " have cancel a request with order Id #" + orderId + ":" + chosenPost.ProductName + ".\nReason: Another order related to this product has been completed.\nSorry for this inconvienience.\nPlease, check your exchange request for tracking progress." + "\nHave a nice day!";
                            request.Content = content.ToString();
                            _emailService.SendMail(request);
                        }
                        catch (Exception ex)
                        {
                            return BadRequest("Cannot send email");
                        }
                    }
                    //send email to seller that the exchange is complete
                    try
                    {
                        SendMailModel request = new SendMailModel();
                        request.ReceiveAddress = seller.Email;
                        request.Subject = "Completed Exchange Notification";
                        EmailContent content = new EmailContent();
                        content.Dear = "Dear " + seller.Fullname + ",";
                        content.BodyContent = "Your order from " + completeBuyer.Fullname + " for a product: " + chosenPost.ProductName + " has been completed.\nPlease check your exchange order.\nThank you!";
                        request.Content = content.ToString();
                        _emailService.SendMail(request);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("Cannot send email");
                    }
                    return Ok("Successfull ! Thank you for join us.");
                    
                }
            }
        }
    }
}
