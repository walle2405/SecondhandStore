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
                .Include(p => p.PostType)
                .Include(p => p.PostStatus)
                .ToListAsync();
        }
        
        public async Task<Post?> GetPostById(int id)
        {
            return _postRepository.GetAll().Include(p => p.Account).Include(p => p.Category).FirstOrDefault(p => p.PostId == id);
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
        public async Task InActivePost(Post post) {
            await _postRepository.InactivePost(post);
        }

        
    }
}
