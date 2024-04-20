using Byhands.Models.Bases;
using Microsoft.AspNetCore.Http;

namespace Byhands.Application.Interfaces.Providers;

public interface IUploadService
{
    Task<Result<string>> UploadFile(IFormFile file);
}
