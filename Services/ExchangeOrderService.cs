using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using SecondhandStore.Models;
using SecondhandStore.Repository;

namespace SecondhandStore.Services;

public class ExchangeOrderService
{
    private readonly ExchangeOrderRepository _exchangeOrderRepository;
    private readonly IConfiguration _configuration;
    public ExchangeOrderService(ExchangeOrderRepository exchangeOrderRepository, IConfiguration configuration)
    {
        _exchangeOrderRepository = exchangeOrderRepository;
        _configuration = configuration;
    }
    public async Task<IEnumerable<ExchangeOrder>> GetAllExchange()
    {
        return await _exchangeOrderRepository.GetAllExchange();
    }
    public async Task<IEnumerable<ExchangeOrder>> GetExchangeRequest(int userId)
    {
        return await _exchangeOrderRepository.GetExchangeRequest(userId);
    }
    public async Task<IEnumerable<ExchangeOrder>> GetExchangeOrder(int userId)
    {
        return await _exchangeOrderRepository.GetExchangeOrder(userId);
    }
    public async Task AddExchangeRequest(ExchangeOrder exchangeOrder)
    {
        await _exchangeOrderRepository.Add(exchangeOrder);
    }
    public async Task UpdateExchange(ExchangeOrder exchangeOrder)
    {
        await _exchangeOrderRepository.Update(exchangeOrder);
    }
    public async Task<ExchangeOrder?> GetExchangeById(int id)
    {
        return await _exchangeOrderRepository.GetByIntId(id);
    }
    public async Task<IEnumerable<ExchangeOrder>> GetExchangeByPostId(int userId, int postId)
    {
        var order = await _exchangeOrderRepository.GetAll().Where(p => p.BuyerId == userId && p.PostId == postId).ToListAsync();
        return order;
    }
    public async Task<IEnumerable<ExchangeOrder>> GetAllRelatedProductPost(int postId, int orderid)
    {
        return await _exchangeOrderRepository.GetAllRelatedProductPost(postId, orderid);
    }
    public async Task<IEnumerable<ExchangeOrder>> GetExchangesListByPostId(int postId)
    {
        return await _exchangeOrderRepository.GetExchangeListByPostId(postId);
    }
    public async Task<IEnumerable<Post>> GetPurchasedPost(int buyerId)
    {
        return await _exchangeOrderRepository.GetPurchasedPost(buyerId);
    }
}