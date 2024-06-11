using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Project.PL.Utilities
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. get located folder
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
            // 2. get file name and make it unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}"; 
            // 3. get the file paht[folder path + fileName]
            string filePath = Path.Combine(folderPath, fileName);
            // 4. save file as stream
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            // return file name
            return fileName;
        }
    }
}
