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
                .Include(p => p.Category)
                .Include(p => p.PostStatus)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Post>> GetAllActivePosts()
        {
            return await _postRepository.GetAll()
                .Include(p => p.Account)
                .Include(p => p.Category)
                .Include(p => p.PostStatus)
                .Where(p => p.PostStatusId == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<Post>?> GetPostByAccountId(int id)
        {
            return await _postRepository.GetAll()
            .Where(p => p.AccountId == id)
            .Include(p => p.Account)
            .Include(p => p.Category)
            .Include(p => p.PostStatus)
            .ToListAsync();
        }

        public async Task<Post?> GetPostById(int id)
        {
            return _postRepository.GetAll()
            .Include(p => p.Account)
            .Include(p => p.Category)
            .Include(p => p.PostStatus)
            .FirstOrDefault(p => p.PostId == id);
        }

        public async Task AddPost(Post p)
        {
            await _postRepository.Add(p);
        }
        public async Task<IEnumerable<Post>> GetPostByProductName(string productName)
        {
            return await _postRepository.GetPostByProductName(productName);

        }
        public async Task UpdatePost(Post post)
        {
            await _postRepository.Update(post);
        }
        
        public async Task AcceptPost(Post acceptedPost) { 
            await _postRepository.AcceptPost(acceptedPost);
        }
        public async Task RejectPost(Post rejectedPost) { 
            await _postRepository.RejectPost(rejectedPost);
        }
    }
}
