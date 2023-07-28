using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Models;
using SecondhandStore.Services;

namespace SecondhandStore.Controllers
{
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        private readonly IMapper _mapper;
        public ReviewController(ReviewService reviewService, IMapper mapper) {
            _reviewService = reviewService;
            _mapper = mapper;
        }
        [HttpPost("submit-review")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> SubmitReviewForUser(ReviewCreateRequest reviewCreateRequest) {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            int parseUserId = Int32.Parse(userId);
            var mappedReview = _mapper.Map<Review>(reviewCreateRequest);
            mappedReview.ReviewerId = parseUserId;
            mappedReview.CreatedDate = DateTime.Now;
            await _reviewService.AddReview(mappedReview);
            return Ok("Send successfully!");
        }
    }
    
}
