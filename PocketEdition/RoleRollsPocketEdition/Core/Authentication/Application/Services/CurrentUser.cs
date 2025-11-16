using RoleRollsPocketEdition.Core.Authentication.Dtos;

namespace RoleRollsPocketEdition.Core.Authentication.Application.Services
{
    public class CurrentUser : ICurrentUser
    {
        public UserModel User { get; set; } = new();
    }
}
