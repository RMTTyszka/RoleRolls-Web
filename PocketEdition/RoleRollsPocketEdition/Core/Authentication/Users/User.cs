using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Core.Authentication.Users
{
    public class User : Entity
    {
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public void HashPassword(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashed = BCrypt.Net.BCrypt.HashPassword(password, salt);
            Password = hashed;
        }
        public bool Challenge(string password)
        {
            var valid = BCrypt.Net.BCrypt.Verify(password, Password);
            return valid;
        }
    }
}
