﻿using SecondhandStore.Models;
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
            return await _postRepository.GetAll().Include(p => p.Account).Include(p => p.Category).ToListAsync();
        }
        
        public async Task<Post> GetPostById(int id)
        {
            return await _postRepository.GetByIntId(id);
        }

        public async Task<Post> AddPost(Post p)
        {
            await _postRepository.Add(p);
            return await _postRepository.GetByIntId(p.PostId);
        }
    }
}
