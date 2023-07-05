using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using SecondhandStore.Custom;

namespace SecondhandStore.Repository;

public class AzureStorageRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _storageConnectionString;

    public AzureStorageRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _storageConnectionString = configuration.GetValue<string>("Azure");
    }

    public async Task<BlobResponse?> UpdateFileAsync(IFormFile? file, string? fileName, string containerName,
        string? fileExtension, bool isPrivate)
    {
        BlobResponse? response = new();

        // Get a reference to a container named in appsettings.json and then create it
        var blogService = new BlobServiceClient(_storageConnectionString);

        var containerClient = blogService.GetBlobContainerClient(containerName);
            
        switch (isPrivate)
        {
             case true:
                 await containerClient.CreateIfNotExistsAsync();
                 break;
             case false:
                 await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
                 break;
        }

        try
        {
            var fileToDelete = containerClient.GetBlobClient(fileName);
            
            // check if the file exists
            if (file == null)
                return response = null;
            try
            {
                // delete current file to upload new one if exist
                await fileToDelete.DeleteIfExistsAsync();

            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                // File did not exist, log to console and return new response to requesting method
               
            }
            // Get a reference to the blob just uploaded from the API in a container from configuration settings

            var uniqueId = Guid.NewGuid();

            var client = containerClient.GetBlobClient(uniqueId + file.FileName);

            // Open a stream for the file we want to upload
            await using (var data = file.OpenReadStream())
            {
                // Upload the file async
                await client.UploadAsync(data,
                    new BlobHttpHeaders
                    {
                        // fileExtension should be image/{extension}
                        ContentType = $"image/{fileExtension}"
                    });
            }

            // Everything is OK and file got uploaded
            response.Status = $"File {file.FileName} uploaded Successfully";
            response.Error = false;
            response.Blob.Uri = client.Uri.AbsoluteUri;
            response.Blob.Name = client.Name;
        }
        // If the file already exists, we catch the exception and do not upload it
        
        // If we get an unexpected error, we catch it here and return the error message
        catch (RequestFailedException ex)
        {
            response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
            response.Error = true;
            return response;
        }

        // Return the BlobUploadResponse object
        return response;
    }
    
}

