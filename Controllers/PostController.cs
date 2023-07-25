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
        private readonly AccountService _accountService;
        private readonly ImageService _imageService;

        public PostController(PostService postService, IMapper mapper, AzureService azureService,
            AccountService accountService, ImageService imageService)
        {
            _postService = postService;
            _mapper = mapper;
            _azureService = azureService;
            _accountService = accountService;
            _imageService = imageService;
        }

        [HttpGet("get-post-list")]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> GetPostList()
        {
            var postList = await _postService.GetAllPosts();

            if (!postList.Any())
                return NotFound();

            var mappedPostList = postList.Select(c => _mapper.Map<PostEntityViewModel>(c));
            return Ok(mappedPostList);
        }

        [HttpGet("get-all-active-post-list")]
        public async Task<IActionResult> GetActivePostList()
        {
            var postList = await _postService.GetAllActivePosts();

            if (!postList.Any())
                return NotFound();

            var mappedPostList = postList.Select(c => _mapper.Map<PostEntityViewModel>(c));
            return Ok(mappedPostList);
        }

        [HttpGet("get-user-posts")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> GetPostsByUserId()
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ??
                         string.Empty;

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
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ??
                         string.Empty;
            var postList = await _postService.GetPostByAccountId(Int32.Parse(userId));
            var post = postList.FirstOrDefault(p => p.PostId == id);
            if (post is null)
                return NotFound();
            var mappedPost = _mapper.Map<PostEntityViewModel>(post);
            return Ok(mappedPost);
        }

        [HttpGet("get-post-by-id")]
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
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ??
                         string.Empty;

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

            var postId = await _postService.AddPost(createdPost, int.Parse(userId));

            if (postId == 0)
                return BadRequest();

            var imageUrls = new List<string?>();

            foreach (var image in postCreateRequest.ImageUploadRequest)
            {
                var imageExtension = ImageExtension.ImageExtensionChecker(image.FileName);

                //var fileNameCheck = createdPost.Images.Split('/').LastOrDefault();

                var uri = (await _azureService.UploadImage(image, null, "post", imageExtension, false))?.Blob.Uri;

                imageUrls.Add(uri);
            }

            await _imageService.AddImage(imageUrls, postId);

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
        public async Task<IActionResult> AcceptPost(int id)
        {
            var existingPost = await _postService.GetPostById(id);
            if (existingPost is null)
            {
                return NotFound();
            }
            else
            {
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
            else
            {
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
            if (postId == 0)
                return BadRequest();
            try
            {
                var existingPost = await _postService.GetPostById(postId);
            
                if (existingPost is null)
                    return NotFound();
            
                var mappedPost = _mapper.Map<Post>(postUpdateRequest);
                mappedPost.AccountId = Int32.Parse(userId);
                mappedPost.PostId = existingPost.PostId;
                mappedPost.CategoryId = existingPost.CategoryId;

                var imageUrls = new List<string?>();
                    foreach (var image in postUpdateRequest.ImageUploadRequest)
                    {
                        var imageExtension = ImageExtension.ImageExtensionChecker(image.FileName);
                        // var fileNameCheck = mappedPost.Image?.Split('/').LastOrDefault();
            
                        var uri = (await _azureService.UploadImage(image, null, "post", imageExtension, false))?.Blob.Uri;
            
                        imageUrls.Add(uri);
                    }
                await _postService.UpdatePost(mappedPost);
                await _imageService.UpdateImage(imageUrls, postId);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Invalid Request");
            }
            return Ok();
        }

        [HttpPut("deactivate-own-post")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> DeactivatePost(int postId)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ??
                         string.Empty;
            try
            {
                var existingPost = await _postService.GetPostById(postId);

                if (existingPost is null)
                    return NotFound();

                existingPost.AccountId = Int32.Parse(userId);
                existingPost.PostStatusId = 8;
                await _postService.UpdatePost(existingPost);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("Post cannot be deactivated");
            }
        }
    }
}