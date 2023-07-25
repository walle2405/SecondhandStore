using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondhandStore.EntityRequest;
using SecondhandStore.EntityViewModel;
using SecondhandStore.Extension;
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
        private readonly AzureService _azureService;
        private readonly ReportImageService _reportImageService;

        public ReportController(IMapper mapper, ReportService reportService, AzureService azureService,
            ReportImageService reportImageService)
        {
            _mapper = mapper;
            _reportService = reportService;
            _azureService = azureService;
            _reportImageService = reportImageService;
        }

        [HttpGet("get-report-list-in-admin-dashboard")]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> GetReportList()
        {
            var reportList = await _reportService.GetAllReport();

            var mappedReportList = reportList.Select(c=>_mapper.Map<ReportEntityViewModel>(c));
            if (!reportList.Any())
                return NotFound();

            return Ok(mappedReportList);
        }

        [HttpPost("sending-report-form")]
        [Authorize(Roles = "US")]
        public async Task<IActionResult> SendRequest([FromForm]ReportCreateRequest reportCreateRequest)
        {
            var userId = User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "accountId")?.Value ??
                         string.Empty;

            int parseUserId = Int32.Parse(userId);

            var mappedReport = _mapper.Map<Report>(reportCreateRequest);

            mappedReport.ReporterId = parseUserId;

            mappedReport.ReportDate = DateTime.Now;

            mappedReport.ReportStatusId = 6;

            var reportId = await _reportService.AddReport(mappedReport);

            if (reportId == 0)
                return BadRequest();
            
            var imageUrls = new List<string?>();

            foreach (var image in reportCreateRequest.ImageUploadRequest)
            {
                var imageExtension = ImageExtension.ImageExtensionChecker(image.FileName);

                //var fileNameCheck = createdPost.Images.Split('/').LastOrDefault();

                var uri = (await _azureService.UploadImage(image, null, "post", imageExtension, false))?.Blob.Uri;

                imageUrls.Add(uri);
            }

            await _reportImageService.AddImage(imageUrls, reportId);
            
            return Ok();
        }

        [HttpPut("accept-report")]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> AcceptedReport(int reportId)
        {
            var report = await _reportService.GetReportById(reportId);
            if (report is null)
            {
                return NotFound();
            }
            else
            {
                if (report.ReportStatusId == 4)
                {
                    return NoContent();
                }
                else
                {
                    await _reportService.AcceptReport(report);
                    return NoContent();
                }
            }
        }

        [HttpPut("reject-report")]
        [Authorize(Roles = "AD")]
        public async Task<IActionResult> RejectReport(int reportId)
        {
            var report = await _reportService.GetReportById(reportId);
            if (report is null)
            {
                return NotFound();
            }
            else
            {
                if (report.ReportStatusId == 5)
                {
                    return NoContent();
                }
                else
                {
                    await _reportService.RejectReport(report);
                    return NoContent();
                }
            }
        }
    }
}