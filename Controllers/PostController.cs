﻿using AutoMapper;
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

        [HttpGet("get-post-list")]
        public async Task<IActionResult> GetPostList()
        {
            var postList = await _postService.GetAllPosts();

            if (!postList.Any())
                return NotFound();

            return Ok(postList);
        }
        
        [HttpPost("create-new-post")]
        public async Task<IActionResult> CreateNewPost(PostCreateRequest postCreateRequest)
        {
            var mappedPost = _mapper.Map<Post>(postCreateRequest);

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
            return Ok(existingPost);
        }

        [HttpPut("toggle-post-status")]
        public async Task<IActionResult> TogglePostStatus(int id)
        {
            try
            {
                var existingPost = await _postService.GetPostById(id);

                if (existingPost is null)
                    return NotFound();

                existingPost.PostStatus = !existingPost.PostStatus;

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
        public async Task<IActionResult> UpdatePost(int postId, PostUpdateRequest postUpdateRequest)
        {
            try
            {
                var existingPost = await _postService.GetPostById(postId);

                if (existingPost is null)
                    return NotFound();

                var mappedPost = _mapper.Map<Post>(postUpdateRequest);

                await _postService.UpdatePost(mappedPost);

                return Ok(mappedPost);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Invalid Request");
            }
        }
    }
}