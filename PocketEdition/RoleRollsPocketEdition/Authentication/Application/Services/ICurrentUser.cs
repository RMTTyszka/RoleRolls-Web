using RoleRollsPocketEdition.Authentication.Dtos;

namespace RoleRollsPocketEdition.Authentication.Application.Services
{
    public interface ICurrentUser
    {
        UserModel User { get; set; }
    }
}