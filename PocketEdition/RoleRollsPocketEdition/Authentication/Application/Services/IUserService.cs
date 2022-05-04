using RoleRollsPocketEdition.Authentication.Users;

namespace RoleRollsPocketEdition.Authentication.Application.Services
{
    public interface IUserService
    {
        Task<User> Get(Guid id);
        Task CreateAsync(User user);
        Task<string> LoginAsync(string email, string password);
    }
}