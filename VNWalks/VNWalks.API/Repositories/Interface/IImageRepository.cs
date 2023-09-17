using VNWalks.API.Models.Domain;

namespace VNWalks.API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
