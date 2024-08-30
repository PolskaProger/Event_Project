using System.Threading.Tasks;
using EventsProject.Application.Interfaces;
using EventsProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Events_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            var email = await _authService.RegisterAsync(model.FirstName, model.LastName, model.BirthDate, model.Email, model.Password);
            return Ok(new { Email = email });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            var tokens = await _authService.LoginAsync(model.Email, model.Password);
            return Ok(tokens);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh([FromBody] RefreshRequest model)
        {
            var tokens = await _authService.RefreshTokenAsync(model.Token, model.RefreshToken);
            return Ok(tokens);
        }
    }

    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RefreshRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
