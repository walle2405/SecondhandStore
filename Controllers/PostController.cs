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

        [HttpGet("get-user-posts/")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> GetPostsByUserId()
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            var postList = await _postService.GetPostByAccountId(Int32.Parse(userId));
            if (postList is null)
                return NotFound();
            var mappedPostList = postList.Select(c => _mapper.Map<PostEntityViewModel>(c));
            return Ok(mappedPostList);
        }

        [HttpGet("get-user-post-by-id")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> GetPostByUserId(int id)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            var postList = await _postService.GetPostByAccountId(Int32.Parse(userId));
            var post = postList.FirstOrDefault(p => p.PostId == id);
            if (post is null)
                return NotFound();
            var mappedPost = _mapper.Map<PostEntityViewModel>(post);
            return Ok(mappedPost);
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
        [Authorize(Roles = "US")]
        public async Task<IActionResult> CreateNewPost([FromForm] PostCreateRequest postCreateRequest)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;

            var createdPost = new Post
            {
                AccountId = Int32.Parse(userId),
                CreatedDate = DateTime.Now,
                ProductName = postCreateRequest.ProductName,
                Description = postCreateRequest.Description,
                PostStatusId = postCreateRequest.PostStatusId,
                CategoryId = postCreateRequest.CategoryId,
                IsDonated = postCreateRequest.isDonated,
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

        [HttpPut("accept-post")]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> AcceptPost(int id) {
            var existingPost = await _postService.GetPostById(id);
            if (existingPost is null)
            {
                return NotFound();
            }
            else {
                if (existingPost.PostStatusId == 4)
                {
                    return NoContent();
                }
                else 
                {
                    await _postService.AcceptPost(existingPost);
                    return NoContent();
                }  
            }
        }
        [HttpPut("reject-post")]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> RejectPost(int id)
        {
            var existingPost = await _postService.GetPostById(id);
            if (existingPost is null)
            {
                return NotFound();
            }
            else {
                if (existingPost.PostStatusId == 5)
                {
                    return NoContent();
                }
                else 
                {
                    await _postService.RejectPost(existingPost);
                    return NoContent();
                }  
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