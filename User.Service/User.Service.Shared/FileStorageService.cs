using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using User.Service.Application.Abstractions;

namespace User.Service.Shared
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderPath)
        {
            var folder = folderPath + "/" + Guid.NewGuid().ToString() + "_" + file.FileName;
            var serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folder;
        }
    }
}
