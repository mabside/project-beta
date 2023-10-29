using Microsoft.AspNetCore.Http;
using Review.Models.Bases;

namespace Review.Application.Interfaces.Providers;

public interface IUploadService
{
    Task<Result<string>> UploadFile(IFormFile file);
}
