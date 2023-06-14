using SecondhandStore.Models;
using SecondhandStore.Repository;

namespace SecondhandStore.Services
{
    public class TopUpService
    {
        private readonly TopUpRepository _topupRepository;

        public TopUpService(TopUpRepository topupRepository)
        {
            _topupRepository = topupRepository;
        }

        public async Task<List<TopUp>> GetAllTopUp()
        {
            return await _topupRepository.GetAll();
        }

        public async Task<TopUp> GetTopUpById(int topupid)
        {
            return await _topupRepository.GetByIntId(topupid);
        }

        public async Task AddTopUp(TopUp topUp)
        {
            await _topupRepository.Add(topUp);
        }
    }
}

