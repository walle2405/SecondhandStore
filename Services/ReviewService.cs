using SecondhandStore.Models;
using SecondhandStore.Repository;

namespace SecondhandStore.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepository;
        private readonly IConfiguration _configuration;
        public ReviewService(ReviewRepository reviewRepository, IConfiguration configuration) 
        {
            _reviewRepository = reviewRepository;
            _configuration = configuration;
        }
        public async Task<IEnumerable<Review>> GetAllReviewsByReviewedId(int reviewedId) 
        {
            return await _reviewRepository.GetAllReviewbyReviewedId(reviewedId);     
        }
        public async Task AddReview(Review review) {
            await _reviewRepository.Add(review);
        }
    }
}
