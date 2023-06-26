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

        public async Task<List<Post>> GetAllPosts()
        {
            return await _postRepository.GetAll().Include(p => p.Account).ToListAsync();
        }
        
        public async Task<Post> GetPostById(int id)
        {
            return await _postRepository.GetAll().Include(p => p.Account).FirstOrDefaultAsync(p => p.PostId == id);
        }

        public async Task<Post> AddPost(Post p)
        {
            await _postRepository.Add(p);
            return await _postRepository.GetByIntId(p.PostId);
        }
        public async Task<Post> GetSinglePost(int i)
        {
            return await _postRepository.GetByIntId(i);
        }
    }
}
