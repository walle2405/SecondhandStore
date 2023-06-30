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
        return await _dbContext.Posts.Where(c => c.ProductName.ToLower().Contains(ProductName.ToLower()))
            .Include(p => p.Account).Include(p => p.Category).ToListAsync();
            
    }
}