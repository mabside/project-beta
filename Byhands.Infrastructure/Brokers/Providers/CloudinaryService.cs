using Byhands.Abstractions.Providers;
using Byhands.Infrastructure.Options;
using Byhands.Models.Bases;
using Byhands.Utilities;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Error = Byhands.Models.Bases.Error;

namespace Byhands.Infrastructure.Brokers.Providers;

public class CloudinaryService : IUploadService
{
    private readonly CloudinarySettings settings;

    public CloudinaryService(IOptions<CloudinarySettings> settings)
    {
        this.settings = settings.Value;
    }

    public async Task<Result<string>> UploadFile(IFormFile file)
    {
        Cloudinary cloudinary = new Cloudinary(this.settings.Url);
        cloudinary.Api.Secure = true;

        var savedFileResult = await FileUtil.SaveFileToTempLocation(file);

        var fileType = savedFileResult.fileType;
        var filePath = savedFileResult.filePath;

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(filePath),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = true
        };

        try
        {
            var uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
        catch (Exception ex)
        {
            return new Error(ex.Message, "Upload.Fail", false);
        }
    }
}
