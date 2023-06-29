using Microsoft.EntityFrameworkCore;
using SecondhandStore.Models;
using SecondhandStore.Repository;

namespace SecondhandStore.Services;

public class ExchangeOrderService
{
    private readonly ExchangeOrderRepository _exchangeOrderRepository;
    public ExchangeOrderService(ExchangeOrderRepository exchangeOrderRepository)
    {
        _exchangeOrderRepository = exchangeOrderRepository;
    }
    public async Task<IEnumerable<ExchangeOrder>> GetAllRequest()
    {
        return await _exchangeOrderRepository.GetAll().ToListAsync();
    }
    public async Task<ExchangeOrder?> GetOrderById(int orderId)
    {
        return await _exchangeOrderRepository.GetByIntId(orderId);
    }
    public async Task AddOrder(ExchangeOrder exchangeOrder)
    {
        await _exchangeOrderRepository.Add(exchangeOrder);
    }
    public async Task UpdateOrder(ExchangeOrder exchangeOrder) 
    { 
        await _exchangeOrderRepository.Update(exchangeOrder);
    }
}