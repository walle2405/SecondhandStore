using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.Custom;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Extension;
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
        private readonly AzureService _azureService;

        public PostController(PostService postService, IMapper mapper, AzureService azureService)
        {
            _postService = postService;
            _mapper = mapper;
            _azureService = azureService;
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

        [HttpGet("get-user-post/")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> GetPostByUserId()
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId") ?.Value ?? string.Empty;
            var postList = await _postService.GetPostByAccountId(Int32.Parse(userId));
            if (postList is null)
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
            var mappedPost = _mapper.Map<PostEntityViewModel>(post);
            return Ok(mappedPost);
        }


        [HttpPost("create-new-post")]
        public async Task<IActionResult> CreateNewPost([FromForm] PostCreateRequest postCreateRequest)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;

            var createdPost = new Post
            {
                AccountId = int.Parse(userId),
                PostDate = DateTime.Now,
                ProductName = postCreateRequest.ProductName,
                Description = postCreateRequest.Description,
                PostStatusId = postCreateRequest.PostStatusId,
                CategoryId = postCreateRequest.CategoryId,
                PostTypeId = postCreateRequest.PostTypeId,
                Price = postCreateRequest.Price
            };

            if (postCreateRequest.ImageUploadRequest != null)
                foreach (var image in postCreateRequest.ImageUploadRequest)
                {
                    var imageExtension = ImageExtension.ImageExtensionChecker(image.FileName);
                    var fileNameCheck = createdPost.Image?.Split('/').LastOrDefault();

                    var uri = (await _azureService.UploadImage(image, fileNameCheck, "post", imageExtension, false))?.Blob.Uri;

                    createdPost.Image = uri;
                }

            Console.Write(createdPost);

            await _postService.AddPost(createdPost);

            // return CreatedAtAction(nameof(GetPostList),
            //     new { id = CreatedPost.AccountId },
            //     CreatedPost;
            return Ok();

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
        public async Task<IActionResult> UpdatePost(int postId, [FromForm] PostUpdateRequest postUpdateRequest)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            try
            {
                var existingPost = await _postService.GetPostById(postId);

                if (existingPost is null)
                    return NotFound();

                var mappedPost = _mapper.Map<Post>(postUpdateRequest);
                mappedPost.AccountId = Int32.Parse(userId);
                mappedPost.PostId = existingPost.PostId;
                mappedPost.CategoryId = existingPost.CategoryId;


                if (postUpdateRequest.ImageUploadRequest != null)
                    foreach (var image in postUpdateRequest.ImageUploadRequest)
                    {
                        var imageExtension = ImageExtension.ImageExtensionChecker(image.FileName);
                        var fileNameCheck = mappedPost.Image?.Split('/').LastOrDefault();

                        var uri = (await _azureService.UploadImage(image, fileNameCheck, "post", imageExtension, false))?.Blob.Uri;

                        mappedPost.Image = uri;
                    }
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