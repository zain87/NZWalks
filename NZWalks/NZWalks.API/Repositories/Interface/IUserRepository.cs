using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string UserName, string Password);
    }
}
