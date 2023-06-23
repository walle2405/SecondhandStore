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
        public async Task<IActionResult> GetPostList()
        {
            var postList = await _postService.GetAllPosts();

            if (postList.Count == 0 || !postList.Any())
                return NotFound();

            return Ok(postList);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSinglePost(int id)
        {
            var post = await _postService.GetSinglePost(id);

            if(post is null) return NotFound();
            return Ok(post);
        }
    }
}
