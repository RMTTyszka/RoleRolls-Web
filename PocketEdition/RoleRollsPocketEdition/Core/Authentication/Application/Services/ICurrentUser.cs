using RoleRollsPocketEdition.Core.Authentication.Dtos;

namespace RoleRollsPocketEdition.Core.Authentication.Application.Services
{
    public interface ICurrentUser
    {
        UserModel User { get; set; }
    }
}