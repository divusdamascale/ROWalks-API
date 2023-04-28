using ROWalks.API.Data;
using ROWalks.API.Models.Domain;

namespace ROWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ROWalksDbContext db;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor
            ,ROWalksDbContext db)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.db = db;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath,"Images",
                $"{image.FileName}{image.FileExtension}");

            //upload Image to local Path
            using var stream = new FileStream(localFilePath,FileMode.Create);
            await image.File.CopyToAsync(stream);

            //

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            //Add Image to the Images table

            await db.Images.AddAsync(image);
            await db.SaveChangesAsync();

            return image;
        }
    }
}
