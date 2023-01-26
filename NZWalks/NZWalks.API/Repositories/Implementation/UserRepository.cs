using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        List<User> users = new List<User>()
        {
            new User()
            {
                FirstName = "Zain", LastName = "Abbas", EmailAddress = "azain@sofcom.net",
                Id = Guid.NewGuid(), UserName = "azain@sofcom.net", Password = "Sofcom123",
                Roles = new List<string> { "reader" }
            },
            new User()
            {
                FirstName = "Raza", LastName = "Haider", EmailAddress = "hraza@sofcom.net",
                Id = Guid.NewGuid(), UserName = "hraza@sofcom.net", Password = "Sofcom678",
                Roles = new List<string> { "reader", "writer" }
            }
        };

        public async Task<User> Authenticate(string UserName, string Password)
        {
            var user = users.Find(x => x.UserName.Equals(UserName, StringComparison.InvariantCultureIgnoreCase) && x.Password == Password);
            return user;
        }
    }
}
