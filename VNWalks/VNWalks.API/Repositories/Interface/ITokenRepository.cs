﻿using Microsoft.AspNetCore.Identity;

namespace VNWalks.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
