using Microsoft.AspNetCore.Http;
using Byhands.Models.Bases;

namespace Byhands.Application.Interfaces.Providers;

public interface IUploadService
{
    Task<Result<string>> UploadFile(IFormFile file);
}
