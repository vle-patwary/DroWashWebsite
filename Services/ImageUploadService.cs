namespace DroWashWebsite.Services
{
    public class ImageUploadService
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

        private readonly IWebHostEnvironment _env;

        public ImageUploadService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public (bool IsValid, string? ErrorMessage) Validate(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return (false, "Please choose an image file.");
            }

            if (file.Length > MaxFileSizeBytes)
            {
                return (false, "Image is too large. Maximum size is 5 MB.");
            }

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(ext))
            {
                return (false, "Only .jpg, .jpeg, .png and .webp files are allowed.");
            }

            return (true, null);
        }

        /// <summary>
        /// Saves the uploaded file into wwwroot/images/{folder}/ with a unique
        /// name, and returns the relative URL to store in the database.
        /// </summary>
        public async Task<string> SaveAsync(IFormFile file, string folder)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "images", folder);
            Directory.CreateDirectory(uploadsFolder);

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/{folder}/{fileName}";
        }

        /// <summary>
        /// Deletes a previously saved image given its relative URL
        /// (e.g. "/images/hero-slideshow/abc123.jpg"). Safe to call even if
        /// the file no longer exists.
        /// </summary>
        public void Delete(string? relativeUrl)
        {
            if (string.IsNullOrWhiteSpace(relativeUrl))
            {
                return;
            }

            var relativePath = relativeUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
            var fullPath = Path.Combine(_env.WebRootPath, relativePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}