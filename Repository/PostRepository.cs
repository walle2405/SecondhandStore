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
                .Include(p => p.Images)
                .Include(p => p.Account)
                .Include(p => p.Category)
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
            existingPost.Description = updatedPost.Description;
            existingPost.Price = updatedPost.Price;
            existingPost.CreatedDate = DateTime.Today;
            await _dbContext.SaveChangesAsync();
        }
    }
    
    public async Task AcceptPost(Post acceptedPost) {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(a => a.PostId == acceptedPost.PostId);
        if (post != null)
        {
            post.PostStatusId = 4;
        }
        await _dbContext.SaveChangesAsync();
    }
    public async Task RejectPost(Post rejectedPost)
    {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(a => a.PostId == rejectedPost.PostId);
        if (post != null)
        {
            post.PostStatusId = 5;
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddNewPost(Post post, int accountId)
    {
        var category = await _dbContext.Categories.FindAsync(post.CategoryId);
        var account = await _dbContext.Accounts.FindAsync(accountId);
        account.PointBalance -= category.CategoryValue;
        _dbContext.Posts.Add(post);
        await _dbContext.SaveChangesAsync();
    }
}