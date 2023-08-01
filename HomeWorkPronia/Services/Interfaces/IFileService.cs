using Microsoft.AspNetCore.Mvc;

namespace HomeWorkPronia.Services.Interfaces
{
    public interface IFileService
    {
         Task<string> CreateFileAsync(IFormFile file, string path);
         void DeleteFile(string path);    
    }
}
