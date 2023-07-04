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
                .ToListAsync();
        }
        
        public async Task<Post?> GetPostById(int id)
        {
            return await _postRepository.GetByIntId(id);
        }

        public async void AddPost(Post p)
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

        
    }
}
