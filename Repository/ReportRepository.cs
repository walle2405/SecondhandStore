using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;
using SecondhandStore.Models;
using SecondhandStore.Repository.BaseRepository;

namespace SecondhandStore.Repository
{
    public class ReportRepository:BaseRepository<Report>
    {
        private readonly SecondhandStoreContext _dbContext;
        public ReportRepository(SecondhandStoreContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Report>> GetByUserId(string userId)
        {
            return await _dbContext.Reports.Where(c => c.ReporterId.Equals(userId)).ToListAsync();
        }
        public async Task<IEnumerable<Report>> GetAllReport() { 
            return await _dbContext.Reports
                .Include(c => c.Reporter)
                .Include(c=> c.ReportedAccount)
                .Include(c=> c.ReportStatus)
                .Include(c => c.ReportImages)
                .ToListAsync();
        }
        public async Task AcceptReport(Report acceptReport) {
            var report = await _dbContext.Reports.FirstOrDefaultAsync(c => c.ReportId == acceptReport.ReportId);
            if (report != null)
            {
                report.ReportStatusId = 4;
            }
            await _dbContext.SaveChangesAsync();

        }
        public async Task RejectReport(Report rejectReport)
        {
            var report = await _dbContext.Reports.FirstOrDefaultAsync(c => c.ReportId == rejectReport.ReportId);
            if (report != null)
            {
                report.ReportStatusId = 5;
            }
            await _dbContext.SaveChangesAsync();

        }

    }
}
