using SecondhandStore.Models;
using SecondhandStore.Repository;

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
            return await _postRepository.GetAll();
        }

        public async Task AddPost(Post post)
        {
            await _postRepository.Add(post);
        }
        
        public async Task<IEnumerable<Post>> GetPostByProductName(string productName)
        {
            return await _postRepository.GetPostByProductName(productName);
        }
        
        public async Task<Post?> GetPostById(int id)
        {
            return await _postRepository.GetByIntId(id);
        }
        
        public async Task UpdatePost(Post post)
        {
            await _postRepository.Update(post);
        }
    }
}
