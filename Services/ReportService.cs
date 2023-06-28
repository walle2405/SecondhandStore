﻿using Microsoft.EntityFrameworkCore;
using SecondhandStore.Models;
using SecondhandStore.Repository;

namespace SecondhandStore.Services
{
    public class ReportService
    {
        private readonly ReportRepository _reportRepository;
        private readonly IConfiguration _configuration;
        public ReportService(ReportRepository reportRepository, IConfiguration configuration)
        {
            _reportRepository = reportRepository;
            _configuration = configuration;
        }
        public async Task<IEnumerable<Report>> GetAllReport() { 
            return await _reportRepository.GetAll().ToListAsync();
        }
        public async Task<Report?> GetReportById(int id) { 
            return await _reportRepository.GetByIntId(id);
        }
        public async Task<IEnumerable<Report>> GetReportByAccount(string userId) {
            return await _reportRepository.GetByUserId(userId);
        }
        public async Task AddReport(Report report)
        {
            await _reportRepository.Add(report);
        }
    }
}
