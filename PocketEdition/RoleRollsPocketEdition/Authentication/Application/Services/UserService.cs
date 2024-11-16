using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoleRollsPocketEdition.Authentication.Application.Controllers;
using RoleRollsPocketEdition.Authentication.Dtos;
using RoleRollsPocketEdition.Authentication.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Authentication.Application.Services
{
    public class UserService : IUserService
    {
        private readonly RoleRollsDbContext _dbContext;
        private readonly AppSettings _appSettings;

        public UserService(RoleRollsDbContext dbContext, IOptions<AppSettings> appSettings)
        {
            _dbContext = dbContext;
            _appSettings = appSettings.Value;
        }

        public async Task<User> Get(Guid id)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstAsync(e => e.Id == id);
            return user;
        }
        public async Task CreateAsync(User user)
        {
            user.HashPassword(user.Password);
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<LoginResult> LoginAsync(string email, string password)
        {
            var user =  await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            if (user is null)
            {
                return null;
            }

            var valid = user.Challenge(password);
            if (valid) 
            {
                var jwt = GenerateJwtToken(user);
                return new LoginResult
                {
                    UserId = user.Id,
                    UserName = user.Email,
                    Token = jwt
                };
            }
            return null;
        }


        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
