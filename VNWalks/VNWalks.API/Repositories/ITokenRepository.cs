using Microsoft.AspNetCore.Identity;

namespace VNWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
