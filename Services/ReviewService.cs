using Microsoft.EntityFrameworkCore;
using SecondhandStore.Models;
using SecondhandStore.Repository;

namespace SecondhandStore.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepository;
        public ReviewService(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task AddReview(Review review)
        {
            await _reviewRepository.Add(review);
        }

        public async Task<IEnumerable<Review>> GetAllReview()
        {
            return await _reviewRepository.GetAll().ToListAsync();
        }
    }
}
