namespace HomeWorkPronia.Utils
{
    public static class FileUtil
    {
        public static bool CheckFileType(this IFormFile formFile,string type)
        {
            return formFile.ContentType.Contains(type);
        }
        public static bool CheckFileSize(this IFormFile formFile,double size)
        {
            return formFile.Length / 1024 < size;
        }

    }
}
