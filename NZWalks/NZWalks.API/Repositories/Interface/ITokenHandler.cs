using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interface
{
    public interface ITokenHandler
    {
        Task<string> Create(User user);
    }
}
