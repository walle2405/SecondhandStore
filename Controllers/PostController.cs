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
        private readonly AccountService _accountService;
        private readonly IMapper _mapper;
        private readonly AzureService _azureService;

        public PostController(AccountService accountService, PostService postService, IMapper mapper, AzureService azureService)
        {
            _postService = postService;
            _accountService = accountService;
            _mapper = mapper;
            _azureService = azureService;
        }

        [HttpGet("get-post-list")]
        public async Task<IActionResult> GetUserPostsList()
        {
            var postList = await _postService.GetAllPosts();
            postList = postList.Where(p => p.PostStatusId == 3 || p.PostStatusId == 6);
            if (!postList.Any())
                return NotFound();

            var mappedPostList = postList.Select(c => _mapper.Map<PostEntityViewModel>(c));
            return Ok(mappedPostList);
        }

        [HttpGet("get-user-posts")]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> GetAllPosts()
        {
            var postList = await _postService.GetAllPosts();
            if (!postList.Any())
                return NotFound();

            var mappedPostList = postList.Select(c => _mapper.Map<PostEntityViewModel>(c));
            return Ok(mappedPostList);
        }

        [HttpGet("get-own-posts")]
        public async Task<IActionResult> GetPostsByUserId()
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;
            var postList = await _postService.GetPostByAccountId(int.Parse(userId));
            if (!postList.Any())
                return NotFound();

            var mappedPostList = postList.Select(c => _mapper.Map<PostEntityViewModel>(c));
            return Ok(mappedPostList);
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
        public async Task<IActionResult> CreateNewPost([FromForm] PostCreateRequest postCreateRequest)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ?? string.Empty;

            var createdPost = new Post
            {
                AccountId = int.Parse(userId),
                PostDate = DateTime.Now,
                ProductName = postCreateRequest.ProductName,
                Description = postCreateRequest.Description,
                PointCost = postCreateRequest.PointCost,
                PostStatusId = postCreateRequest.PostStatusId,
                CategoryId = postCreateRequest.CategoryId,
                PostTypeId = postCreateRequest.PostTypeId,
                Price = postCreateRequest.Price,
                PostExpiryDate = DateTime.Now.AddDays(5)
            };

            if (postCreateRequest.ImageUploadRequest != null)
                foreach (var image in postCreateRequest.ImageUploadRequest)
                {
                    var imageExtension = ImageExtension.ImageExtensionChecker(image.FileName);
                    var fileNameCheck = createdPost.Image?.Split('/').LastOrDefault();
                    var uri = (await _azureService.UploadImage(image, fileNameCheck, "post", imageExtension, false))?.Blob.Uri;
                    createdPost.Image = uri;
                }


            await _postService.AddPost(createdPost);
            var account = await _accountService.GetAccountById(createdPost.AccountId);
            account.PointBalance -= createdPost.PointCost;
            await _accountService.UpdatePointAutomatic(account);
            // return CreatedAtAction(nameof(GetPostList),
            //     new { id = CreatedPost.AccountId },
            //     CreatedPost;
            return Ok(createdPost.PostId);

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
        public async Task<IActionResult> TogglePostStatus(PostVerifiedRequest pvr)
        {
            try
            {
                var existingPost = await _postService.GetPostById(pvr.id);

                if (existingPost is null)
                    return NotFound();

                // existingPost.IsActive = !existingPost.PostStatus;
                existingPost.PostStatusId = (pvr.choice.Equals("accept") ? 3 : 1);
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
                if (existingPost.AccountId != Int32.Parse(userId))
                {
                    return Unauthorized();
                }
                var mappedPost = _mapper.Map<Post>(postUpdateRequest);
                Console.WriteLine(mappedPost);
                mappedPost.PostId = existingPost.PostId;
                mappedPost.CategoryId = existingPost.CategoryId;
                if(String.IsNullOrEmpty(mappedPost.ProductName)) mappedPost.ProductName = existingPost.ProductName;
                if(String.IsNullOrEmpty(mappedPost.Description)) mappedPost.Description = existingPost.Description;                

                if (postUpdateRequest.ImageUploadRequest != null)
                    foreach (var image in postUpdateRequest.ImageUploadRequest)
                    {
                        var imageExtension = ImageExtension.ImageExtensionChecker(image.FileName);
                        var fileNameCheck = mappedPost.Image?.Split('/').LastOrDefault();

                        var uri = (await _azureService.UploadImage(image, fileNameCheck, "post", imageExtension, false))?.Blob.Uri;

                        mappedPost.Image = uri;
                    }
                else{
                    mappedPost.Image = existingPost.Image;
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