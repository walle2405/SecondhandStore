using SecondhandStore.Models;
using SecondhandStore.Repository;

namespace SecondhandStore.Services;

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
}