using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace fsacwapi.Infrastructure.Data.Storage;

public class StorageService
{
    private const string ProjectId = "fsacwapi";
    private const string ServiceAccountKeyFilePath = ".json";

    public async Task UploadProfilePhotoAsync(Guid userId, string base64ProfilePhoto)
    {
        var credential = GoogleCredential.FromFile(ServiceAccountKeyFilePath);
        var storage = StorageClient.Create(credential);

        var bucketName = "fsacwapi.appspot.com";
        var objectName = $"profile-photos/{userId}.jpg";

        var imageBytes = Convert.FromBase64String(base64ProfilePhoto);
        using var memoryStream = new MemoryStream(imageBytes);

        await storage.UploadObjectAsync(bucketName, objectName, null, memoryStream);
    }

    public async Task<string> GetProfilePhotoUrlAsync(Guid userId)
    {
        var credential = GoogleCredential.FromFile(ServiceAccountKeyFilePath);
        var storage = StorageClient.Create(credential);

        var bucketName = "fsacwapi.appspot.com";
        var objectName = $"profile-photos/{userId}.jpg";

        var storageFilePath = $"profile-photos/{userId}.jpg";

        string downloadUrl = $"https://firebasestorage.googleapis.com/v0/b/{bucketName}/o/{Uri.EscapeDataString(storageFilePath)}?alt=media";

        return downloadUrl;
    }
}
