﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
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

        [HttpGet("get-post-list")]
        public async Task<IActionResult> GetPostList()
        {
            var postList = await _postService.GetAllPosts();

            if (!postList.Any())
                return NotFound();

            var mappedPostList = postList.Select(c => _mapper.Map<PostEntityViewModel>(c));
            return Ok(mappedPostList);
        }

        [HttpGet("get-post-by-id")]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetPostById(id);
            if (post is null)
                return NotFound();
            var mappedPost= _mapper.Map<PostEntityViewModel>(post);
            return Ok(mappedPost);
        }
        
        [HttpPost("create-new-post")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> CreateNewPost(PostCreateRequest postCreateRequest)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId") ?.Value ?? string.Empty;
            
            var mappedPost = _mapper.Map<Post>(postCreateRequest);
            
            mappedPost.AccountId = Int32.Parse(userId);
            
            await _postService.AddPost(mappedPost);

            return CreatedAtAction(nameof(GetPostList),
                new { id = mappedPost.AccountId },
                mappedPost);
        }

        [HttpGet("search-post")]
        public async Task<IActionResult> GetPostByName(string productName)
        {
            var existingPost = await _postService.GetPostByProductName(productName);
            if (existingPost is null)
                return NotFound();
            var mappedExistingPost = existingPost.Select(c => _mapper.Map<PostEntityViewModel>(c));
            return Ok(mappedExistingPost);
        }

        [HttpPut("toggle-post-status")]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> TogglePostStatus(int id)
        {
            try
            {
                var existingPost = await _postService.GetPostById(id);

                if (existingPost is null)
                    return NotFound();
                
                // existingPost.IsActive = !existingPost.PostStatus;

                await _postService.UpdatePost(existingPost);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Invalid Request");
            }

        }

        [HttpPut("update-post")]
        [Authorize(Roles = "AD,US")]
        public async Task<IActionResult> UpdatePost(int postId, PostUpdateRequest postUpdateRequest)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId") ?.Value ?? string.Empty;
            try
            {
                var existingPost = await _postService.GetPostById(postId);

                if (existingPost is null)
                    return NotFound();

                var mappedPost = _mapper.Map<Post>(postUpdateRequest);
                mappedPost.AccountId = Int32.Parse(userId);
                mappedPost.PostId = existingPost.PostId;
                mappedPost.CategoryId = existingPost.CategoryId;
                await _postService.UpdatePost(mappedPost);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Invalid Request");
            }
        }
    }
}