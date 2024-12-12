using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppPMRC.Services
{
    public class FileService
    {
        private readonly string _uploadPath;
        private readonly long _maxFileSize;
        private readonly List<string> _allowedExtensions;

        public FileService(IConfiguration configuration)
        {
            var fileSettings = configuration.GetSection("FileSettings");
            _uploadPath = fileSettings.GetValue<string>("UploadPath");
            _maxFileSize = fileSettings.GetValue<long>("MaxFileSizeInMB") * 1024 * 1024; // Convert to bytes
            _allowedExtensions = fileSettings.GetSection("AllowedExtensions").Get<List<string>>() ?? new List<string>();

            // Create the upload directory if it doesn't exist
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "File cannot be null.");
            }

            // Validate file size
            if (file.Length > _maxFileSize)
            {
                throw new InvalidOperationException($"File size exceeds the limit of {_maxFileSize / (1024 * 1024)} MB.");
            }

            // Validate file extension
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!_allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("File type is not allowed.");
            }

            // Generate a unique filename
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(_uploadPath, uniqueFileName);

            // Save the file
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uniqueFileName; // Return the file name for further use
        }

        public bool DeleteFile(string fileName)
        {
            var fullPath = Path.Combine(_uploadPath, fileName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }

            return false; // File does not exist
        }
    }
}
