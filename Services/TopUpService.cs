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
        return await _topupRepository.GetAll().Include(p=>p.Account).Include(p => p.TopupStatus).ToListAsync();
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
        var topupList = await _topupRepository.GetAll().Where(p=>p.TopupStatusId == 4).ToListAsync();
        if (!topupList.Any())
        {
            return 0;
        }
        double total = topupList.Sum(p => p.Price);
        return total;
    }
    public async Task<Double> GetTotalValueOfUserTransaction(int userId)
    {
        var userTopupList = await _topupRepository.GetAll().Where(p => p.AccountId == userId && p.TopupStatusId == 4).ToListAsync();
        if (!userTopupList.Any())
        {
            return 0;
        }
        double total = userTopupList.Sum(p => p.Price);
        return total;
    }
    public async Task AcceptTopup(TopUp acceptedTopup) { 
        await _topupRepository.AcceptTopup(acceptedTopup);
    }
    public async Task RejectTopup(TopUp rejectedTopup)
    {
        await _topupRepository.RejectTopUp(rejectedTopup);
    }
    public async Task <IEnumerable<TopUp>> GetTopUpByEmail(string searchEmail)
    {
       return await _topupRepository.GetTopUpbyEmail(searchEmail);
    }
}