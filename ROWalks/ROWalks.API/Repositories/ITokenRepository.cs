using Microsoft.AspNetCore.Identity;

namespace ROWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user,List<string> roles);
    }

}
