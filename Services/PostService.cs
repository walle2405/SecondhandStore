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

        public async Task<List<Post>> GetAllPosts()
        {
            return await _postRepository.GetAll();
        }
        public async Task<Post> GetSinglePost(int i)
        {
            return await _postRepository.GetByIntId(i);
        }
    }
}
