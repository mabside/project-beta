using Microsoft.AspNetCore.Http;

namespace Review.Utilities;

public class FileUtil
{
    public static async Task<(string fileType, string filePath)>
        SaveFileToTempLocation(IFormFile file)
    {
        var fileType = file.ContentType;
        var filePath = Path.GetTempFileName();

        using (var stream = File.Create(filePath))
        {
            await file.CopyToAsync(stream);
        }

        return (fileType, filePath);
    }
}
