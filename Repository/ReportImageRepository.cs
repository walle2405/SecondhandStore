using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;
namespace SecondhandStore.Repository;

public class ReportImageRepository : BaseRepository<ReportImage>
{
    private readonly SecondhandStoreContext _dbContext;
    
    public ReportImageRepository(SecondhandStoreContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Add(List<string?> imageUrls, int reportId)
    {
        var existingReport = await _dbContext.Reports.FirstOrDefaultAsync(a => a.ReportId == reportId);

        if (existingReport == null)
        {
            return;
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            if (!imageUrls.Any() || imageUrls.Count == 0)
                return;

            foreach (var newImage in imageUrls.Select(imageUrl => new ReportImage()
                     {
                         ReportId = reportId,
                         ImageUrl = imageUrl,
                     }))
            {
                await _dbContext.ReportImages.AddAsync(newImage);
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