using SecondhandStore.Custom;
using SecondhandStore.Repository;

namespace SecondhandStore.Services;

public class AzureService
{
    private readonly AzureStorageRepository _azureStorageRepository;

    public AzureService(AzureStorageRepository azureStorageRepository)
    {
        _azureStorageRepository = azureStorageRepository;
    }
    
    public async Task<BlobResponse?> UploadImage(IFormFile? file, string? fileName, string containerName,
        string? fileExtension, bool isPrivate)
    {
        return await _azureStorageRepository.UpdateFileAsync(file, fileName, containerName, fileExtension, isPrivate);
    }
}