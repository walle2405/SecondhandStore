using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.Models;
using SecondhandStore.Repository;
using SecondhandStore.Services;

namespace SecondhandStore.Controllers
{
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(ReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/[controller]/get-review-list")]
        public async Task<IActionResult> GetReviewList()
        {
            var reviewList = await _reviewService.GetAllReview();

            if (!reviewList.Any())
                return NotFound();

            return Ok(reviewList);
        }

        [HttpPost]
        [Route("api/[controller]/create-review")]
        public async Task<IActionResult> Review(ReviewRequest reviewRequest)
        {
            var mappedReview = _mapper.Map<Review>(reviewRequest);

            await _reviewService.AddReview(mappedReview);

            return CreatedAtAction(nameof(GetReviewList),
                new { id = mappedReview.ReviewId },
                mappedReview);
        }
    }
}
