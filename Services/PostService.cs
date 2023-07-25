using SecondhandStore.Models;
using SecondhandStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace SecondhandStore.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;

        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;

        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _postRepository.GetAll()
                .Include(p => p.Account)
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p => p.PostStatus)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllActivePosts()
        {
            return await _postRepository.GetAll()
                .Include(p => p.Account)
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p => p.PostStatus)
                .Where(p => p.PostStatusId == 4)
                .ToListAsync();
        }
        public async Task<IEnumerable<Post>?> GetPostByAccountId(int id)
        {
            return await _postRepository.GetAll()
            .Where(p => p.AccountId == id)
            .Include(p => p.Images)
            .Include(p => p.Account)
            .Include(p => p.Category)
            .Include(p => p.PostStatus)
            .ToListAsync();
        }

        public async Task<Post?> GetPostById(int id)
        {
            return _postRepository.GetAll()
            .Include(p => p.Account)
            .Include(p => p.Images)
            .Include(p => p.Category)
            .Include(p => p.PostStatus)
            .FirstOrDefault(p => p.PostId == id);
        }

        /*
        public async Task AddPost(Post post, int accountId)
        {
            await _postRepository.AddNewPost(post, accountId);
        }
        
        */
        
        public async Task<int> AddPost(Post post, int accountId)
        {
            await _postRepository.AddNewPost(post, accountId);
            return post.PostId;
        }
    
        public async Task<IEnumerable<Post>> GetPostByProductName(string productName)
        {
            return await _postRepository.GetPostByProductName(productName);

        }
        public async Task UpdatePost(Post post)
        {
            await _postRepository.Update(post);
        }

        public async Task AcceptPost(Post acceptedPost)
        {
            await _postRepository.AcceptPost(acceptedPost);
        }
        public async Task RejectPost(Post rejectedPost)
        {
            await _postRepository.RejectPost(rejectedPost);
        }
    }
}
