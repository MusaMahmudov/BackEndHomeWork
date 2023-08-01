using HomeWorkPronia.Areas.Admin.ViewModels.CardViewModels;
using HomeWorkPronia.Exceptions;
using HomeWorkPronia.Services.Interfaces;
using HomeWorkPronia.Utils;
using F = System.IO;

namespace HomeWorkPronia.Services.Implementations
{
    public class FileService : IFileService
    {
        public async Task<string> CreateFileAsync(IFormFile file, string path)
        {
            if (!file.CheckFileType("image/"))
            {
                throw new FileTypeException("Shekil Daxil ele");
            }
            if (!file.CheckFileSize(1000))
            {
                throw new FileSizeException("Shekil  cox boyudu");
            }


            string FileName = $"{Guid.NewGuid()}-{file.FileName}";
            string ResultPath = Path.Combine(path, FileName);
            using (FileStream fileStream = new FileStream(ResultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return FileName;
        }

        public  void DeleteFile(string path)
        {
            if(F.File.Exists(path))
            {
                F.File.Delete(path);
            }
        }
    }
}
