namespace GymManagementBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IWebHostEnvironment _webHost;
        public AttachmentService(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long maxFileSize = 5 * 1024 * 1024; // 5 MB

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                // Check for null or empty inputs.
                if (folderName is null || file is null || file.Length == 0) return null;
                //Check size > 5MB
                if (file.Length > maxFileSize) return null;
                // Check Extensions
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(extension)) return null;

                var folderPath = Path.Combine(_webHost.WebRootPath, "images", folderName);
                if (Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Uniqe fileName
                var fileName = Guid.NewGuid().ToString() + extension;

                var filePath = Path.Combine(folderPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);

                return fileName;
            }
            catch (Exception ex) { 
                Console.WriteLine($"Failed To Upload File to folder {folderName} : {ex}");
                return null;
            }
        }

        public bool Delete(string fileName, string folderName)
        {
            try
            {
                if(string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folderName)) 
                    return false;

                var fullPath = Path.Combine(_webHost.WebRootPath, "images", folderName, fileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;

            }
            catch (Exception ex) { 
                Console.WriteLine($"Failed To Delete File with name {fileName}: {ex}");
                return false;
            }
        }

        
    }
}
