using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.Models;
using SecondhandStore.Services;
namespace SecondhandStore.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly IMapper _mapper;
        public PostController(PostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Route("/api/[controller]/get-post-list")]
        public async Task<IActionResult> GetPostList()
        {
            var postList = await _postService.GetAllPosts();

            if (postList.Count == 0 || !postList.Any())
                return NotFound();

            return Ok(postList);
        }
        
        [HttpPost]
        [Route("/api/[controller]/create-new-post")]
        public async Task<IActionResult> CreateNewPost(PostCreateRequest postCreateRequest)
        {
            var mappedPost = _mapper.Map<Post>(postCreateRequest);

            await _postService.AddPost(mappedPost);

            return CreatedAtAction(nameof(GetPostList),
                new { id = mappedPost.AccountId },
                mappedPost);
        }
    }
}
