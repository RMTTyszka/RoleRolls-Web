using RoleRollsPocketEdition.Authentication.Application.Controllers;
using RoleRollsPocketEdition.Authentication.Users;

namespace RoleRollsPocketEdition.Authentication.Application.Services
{
    public interface IUserService
    {
        Task<User> Get(Guid id);
        Task CreateAsync(User user);
        Task<LoginResult> LoginAsync(string email, string password);
    }
}