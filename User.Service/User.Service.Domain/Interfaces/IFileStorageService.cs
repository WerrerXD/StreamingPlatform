using Microsoft.AspNetCore.Http;

namespace User.Service.Domain.Interfaces;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file, string folderPath);
}