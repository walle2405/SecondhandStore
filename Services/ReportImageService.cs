using SecondhandStore.Repository;

namespace SecondhandStore.Services;

public class ReportImageService
{
    private readonly ReportImageRepository _reportImageRepository;

    public ReportImageService(ReportImageRepository reportImageRepository)
    {
        _reportImageRepository = reportImageRepository;
    }
    
    public async Task AddImage(List<string?> image, int reportId)
    {
        await _reportImageRepository.Add(image, reportId);
    }
}