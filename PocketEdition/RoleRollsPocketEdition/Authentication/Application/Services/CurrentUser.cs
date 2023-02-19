using RoleRollsPocketEdition.Authentication.Dtos;

namespace RoleRollsPocketEdition.Authentication.Application.Services
{
    public class CurrentUser : ICurrentUser
    {
        public UserModel User { get; set; }
    }
}
