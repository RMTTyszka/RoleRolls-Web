using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Authentication.Users;

namespace RoleRollsPocketEdition.Authentication.Application.Controllers
{
    [ApiController]
    [Route("users")]
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
        public async Task<ActionResult<LoginResult>> Login(LoginInput input)
        {
            var loginResult = await _userService.LoginAsync(input.Email, input.Password);
            if (loginResult is null) 
            {
                return new UnauthorizedResult();
            }
            return Ok(loginResult);
        }
    }
    public class LoginInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }   
    public class LoginResult
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public Guid UserId{ get; set; }
    }
}
