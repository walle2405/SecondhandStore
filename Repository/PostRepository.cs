using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository;

public class PostRepository : BaseRepository<Post>
{
    private readonly SecondhandStoreContext _dbContext;

    public PostRepository(SecondhandStoreContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Post>> GetPostByProductName(string ProductName)
    {
        return await _dbContext.Posts.Where(c => c.ProductName.ToLower()
                .Contains(ProductName.ToLower()))
                .Include(p => p.Account)
                .Include(p => p.Category)
                .Include(p => p.PostType)
                .Include(p => p.PostStatus)
                .ToListAsync();
            
    }

    // Override update method of base repository
    public new async Task Update(Post updatedPost)
    {
        var existingPost = await _dbContext.Posts.FirstOrDefaultAsync(a => a.PostId == updatedPost.PostId);

        if (existingPost != null)
        {
            existingPost.ProductName = updatedPost.ProductName;
            existingPost.Image = updatedPost.Image ?? existingPost.Image;
            existingPost.Description = updatedPost.Description;
            existingPost.Price = updatedPost.Price;
            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task InactivePost(Post updatePost) {
        var currentDate = DateTime.Now;
        var existingPost = await _dbContext.Posts.FirstOrDefaultAsync(a => a.PostId == updatePost.PostId);
        if (existingPost != null)
        {
            if (currentDate == existingPost.PostExpiryDate) {
                existingPost.PostStatusId = 7;
            }
        }
    }
}