using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.Services;

namespace SecondhandStore.Controllers;

[ApiController]
[Route("posts")]
public class PostController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly PostService _postService;

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
}