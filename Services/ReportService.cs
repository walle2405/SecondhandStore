using Microsoft.EntityFrameworkCore;
using SecondhandStore.Models;
using SecondhandStore.Repository;

namespace SecondhandStore.Services
{
    public class ReportService
    {
        private readonly ReportRepository _reportRepository;
        public ReportService(ReportRepository reportRepository, IConfiguration configuration)
        {
            _reportRepository = reportRepository;
        }
        public async Task<IEnumerable<Report>> GetAllReport()
        {
            return await _reportRepository.GetAll()
                .Include(c => c.Reporter)
                .Include(c => c.ReportedAccount)
                .Include(c => c.ReportStatus)
                .Include(c => c.ReportImages)
                .ToListAsync();
        }
        public async Task<Report?> GetReportById(int id) { 
            return _reportRepository.GetAll()
                .Include(c => c.Reporter)
                .Include(c => c.ReportedAccount)
                .Include(c => c.ReportStatus)
                .Include(c => c.ReportImages)
                .FirstOrDefault(p=>p.ReportId == id);
        }
        public async Task<IEnumerable<Report>?> GetReportByAccount(int userId) {
            return await _reportRepository.GetAll()
                .Where(p => p.ReporterId == userId)
                .Include(c => c.Reporter)
                .Include(c => c.ReportedAccount)
                .Include(c => c.ReportStatus)
                .Include(c => c.ReportImages)
                .ToListAsync();
        }
        public async Task<int> AddReport(Report report)
        {
            await _reportRepository.Add(report);
            return report.ReportId;
        }
        public async Task AcceptReport(Report acceptReport) { 
            await _reportRepository.AcceptReport(acceptReport);
        }
        public async Task RejectReport(Report rejectReport) { 
            await _reportRepository.RejectReport(rejectReport);
        }
    }
}
