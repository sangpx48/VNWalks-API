using Microsoft.AspNetCore.Http.HttpResults;
using VNWalks.API.Data;
using VNWalks.API.Models.Domain;

namespace VNWalks.API.Repositories
{
    public class SQLImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly VNWalksDbContext vNWalksDbContext;

        public SQLImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, VNWalksDbContext vNWalksDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.vNWalksDbContext = vNWalksDbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            //tao duong dan cuc bo de tro den thu muc Images
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath,
                "Images", $"{image.FileName}{image.FileExtention}");


            //Upload Image to LocalPath
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //https://localhost:1234/images/images.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}" +
                $"://" +
                $"{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}" +
                $"{image.FileExtention}";

            image.FilePath = urlFilePath;

            //add image to the Images Table
            await vNWalksDbContext.Images.AddAsync(image);
            await vNWalksDbContext.SaveChangesAsync();

            return image;
        }
    }
}
