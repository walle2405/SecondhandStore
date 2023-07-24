using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository;

public class ImageRepository: BaseRepository<Image>
{
    private readonly SecondhandStoreContext _dbContext;

    public ImageRepository(SecondhandStoreContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    // override add of BaseRepository
    
    public async Task Add(List<string?> imageUrls, int postId)
    {
        var existingPost = await _dbContext.Posts.FirstOrDefaultAsync(a => a.PostId == postId);

        if (existingPost == null)
        {
            return;
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            if (!imageUrls.Any() || imageUrls.Count == 0)
                return;

            foreach (var newImage in imageUrls.Select(imageUrl => new Image()
                     {
                         PostId = postId,
                         ImageUrl = imageUrl,
                     }))
            {
                await _dbContext.Images.AddAsync(newImage);
            }

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine("++++" + e + "++++");
            Console.WriteLine("Roll back");
        }
    }

    public async Task Update(List<string?> imageUrls, int postId)
    {
        var existingPost = await _dbContext.Posts.FirstOrDefaultAsync(a => a.PostId == postId);

        if (existingPost == null)
        {
            return;
        }
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            if (!imageUrls.Any() || imageUrls.Count == 0)
                return;
            
            var existingImages = await _dbContext.Images.Where(i => i.PostId == postId).ToListAsync();
            _dbContext.Images.RemoveRange(existingImages);

            foreach (var newImage in imageUrls.Select(imageUrl => new Image()
                     {
                         PostId = postId,
                         ImageUrl = imageUrl,
                     }))
            {
                await _dbContext.Images.AddAsync(newImage);    
            }
                
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine("++++" + e + "++++");
            Console.WriteLine("Roll back");
        }
    }
}
