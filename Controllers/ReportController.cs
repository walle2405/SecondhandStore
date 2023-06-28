using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Models;
using SecondhandStore.Services;

namespace SecondhandStore.Controllers
{
    
    [ApiController]
    [Route("report")]
    public class ReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ReportService _reportService;

        public ReportController(IMapper mapper, ReportService reportService)
        {
            _mapper = mapper;
            _reportService = reportService;
        }
        [HttpGet]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> GetReportList() {
            var reportList = await _reportService.GetAllReport();
            if (!reportList.Any()) {
                return NotFound();
            }
            var mappedReport = _mapper.Map<List<ReportEntityViewModel>>(reportList);
            return Ok(mappedReport);
        }
        [HttpPost]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> SendRequest(ReportCreateRequest reportCreateRequest) {
            var mappedReport = _mapper.Map<Report>(reportCreateRequest);
            mappedReport.ReportDate = DateTime.Now;
            await _reportService.AddReport(mappedReport);
            return CreatedAtAction(nameof(GetReportList), 
                new { id = mappedReport.ReportId},
                mappedReport);
        }

    }
}
