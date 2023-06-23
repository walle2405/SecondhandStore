using SecondhandStore.Models;
using SecondhandStore.Repository;

namespace SecondhandStore.Services
{
    public class ExchangeRequestService
    {
        private readonly ExchangeRequestRepository _exchangeRequestRepository;

        public ExchangeRequestService(ExchangeRequestRepository exchangeRequestRepository)
        {
            _exchangeRequestRepository = exchangeRequestRepository;
        }

        public async Task<List<ExchangeRequest>> GetAllRequest()
        {
            return await _exchangeRequestRepository.GetAll();
        }

        public async Task<ExchangeRequest> GetRequestById(int requestId)
        {
            return await _exchangeRequestRepository.GetByIntId(requestId);
        }

        public async Task AddRequest(ExchangeRequest exchangeRequest)
        {
            await _exchangeRequestRepository.Add(exchangeRequest);
        }
    }
}