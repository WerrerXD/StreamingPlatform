using Microsoft.AspNetCore.Http;

namespace User.Service.Application.Abstractions;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file, string folderPath);
}