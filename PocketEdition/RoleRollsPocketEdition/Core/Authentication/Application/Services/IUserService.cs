using RoleRollsPocketEdition.Core.Authentication.Application.Controllers;
using RoleRollsPocketEdition.Core.Authentication.Users;

namespace RoleRollsPocketEdition.Core.Authentication.Application.Services
{
    public interface IUserService
    {
        Task<User> Get(Guid id);
        Task CreateAsync(User user);
        Task<LoginResult?> LoginAsync(string email, string password);
    }
}
