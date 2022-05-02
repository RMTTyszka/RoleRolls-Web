using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Authentication.Users;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Authentication.Application.Services
{
    public class UserService : IUserService
    {
        private readonly RoleRollsDbContext _dbContext;

        public UserService(RoleRollsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(User user)
        {
            user.HashPassword(user.Password);
            await _dbContext.AddAsync(user);
        }
        public async Task<bool> LoginAsync(string email, string password)
        {
            var user =  await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            if (user is null)
            {
                return false;
            }

            var valid = user.Challenge(password);
            if (valid) 
            {
                return true;
            }
            return false;
        }
    }
}
