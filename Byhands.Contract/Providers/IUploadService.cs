using Byhands.Models.Bases;
using Microsoft.AspNetCore.Http;

namespace Byhands.Abstractions.Providers;

public interface IUploadService
{
    Task<Result<string>> UploadFile(IFormFile file);
}
