using ROWalks.API.Models.Domain;

namespace ROWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);

    }
}
