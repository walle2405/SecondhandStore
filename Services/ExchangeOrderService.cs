using Microsoft.EntityFrameworkCore;
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
    public async Task<IEnumerable<ExchangeOrder>> GetExchangeRequest(int userId) {
        return await _exchangeOrderRepository.GetExchangeRequest(userId);
    }
    public async Task<IEnumerable<ExchangeOrder>> GetExchangeOrder(int userId)
    {
        return await _exchangeOrderRepository.GetExchangeOrder(userId);
    }
    
}