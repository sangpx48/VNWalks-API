using VNWalks.API.Models.Domain;

namespace VNWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
