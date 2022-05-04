using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Authentication.Users;

namespace RoleRollsPocketEdition.Authentication.Application.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost()]
        public async Task<IActionResult> Create(User user)
        {
            await _userService.CreateAsync(user);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginInput input)
        {
            var jwt = await _userService.LoginAsync(input.Email, input.Password);
            if (jwt is null) 
            {
                return new UnauthorizedResult();
            }
            return Ok(jwt);
        }
    }
    public class LoginInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
