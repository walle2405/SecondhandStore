using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Models;
using SecondhandStore.Repository;

namespace SecondhandStore.Services;

public class TopUpService
{
    private readonly TopUpRepository _topupRepository;
    private readonly IMapper _mapper;

    public TopUpService(TopUpRepository topupRepository)
    {
        _topupRepository = topupRepository;
    }


    public async Task<IEnumerable<TopUp>> GetAllTopUp()
    {
        return await _topupRepository.GetAll().ToListAsync();
    }

    public async Task<TopUp?> GetTopUpById(int topupid)
    {
        return await _topupRepository.GetByIntId(topupid);
    }

    public async Task AddTopUp(TopUp topUp)
    {
        await _topupRepository.Add(topUp);
    }
    public async Task<IEnumerable<TopUp>> GetTopUpByUserId(int userId) { 
        return await _topupRepository.GetUserId(userId);
    }
    public async Task<Double> GetTotalRevenue()
    {
        var topupList = await GetAllTopUp();
        if (!topupList.Any())
        {
            return 0;
        }
        double total = topupList.Sum(p => p.Price);
        return total;
    }
}