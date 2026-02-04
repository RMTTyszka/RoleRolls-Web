using System.Security.Claims;
using RoleRollsPocketEdition.Core.Authentication.Dtos;

namespace RoleRollsPocketEdition.Core.Authentication.Application.Services
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ICurrentUser currentUser)
        {
            if (context.User?.Identity?.IsAuthenticated == true && currentUser.User.Id == Guid.Empty)
            {
                var idClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)
                              ?? context.User.FindFirst("sub")
                              ?? context.User.FindFirst("id");

                if (idClaim != null && Guid.TryParse(idClaim.Value, out var userId))
                {
                    currentUser.User = new UserModel
                    {
                        Id = userId
                    };
                }
            }

            await _next(context);
        }
    }
}
