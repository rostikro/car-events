using Azure.Storage.Blobs;

namespace CarEvents.Services;

public class ImageUploadService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public ImageUploadService(IConfiguration configuration)
    {
        _blobServiceClient = new BlobServiceClient(configuration["AzureStorage:ConnectionString"]);
        _containerName = configuration["AzureStorage:ContainerName"];
    }

    public async Task<string> UploadImageAsync(Stream imageStream, string imageName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = containerClient.GetBlobClient(imageName);
        
        await blobClient.UploadAsync(imageStream, overwrite: true);

        return blobClient.Uri.AbsoluteUri;
    }
}