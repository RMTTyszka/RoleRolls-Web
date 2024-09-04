using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition.Authentication.Users
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }

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
