using EventsProject.Domain.Entities;
using EventsProject.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Events_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthController(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
        {
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Проверка пользователя (для примера используем статические данные)
            if (model.Username == "test" && model.Password == "password")
            {
                var token = GenerateJwtToken(model.Username);
                var refreshToken = GenerateRefreshToken();

                var newRefreshToken = new RefreshToken
                {
                    Token = refreshToken,
                    Username = model.Username,
                    Expires = DateTime.UtcNow.AddDays(double.Parse(_configuration["JwtSettings:RefreshTokenExpiration"])),
                    Created = DateTime.UtcNow,
                    IsRevoked = false
                };

                await _refreshTokenRepository.AddTokenAsync(newRefreshToken);

                return Ok(new { token, refreshToken });
            }

            return Unauthorized();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest model)
        {
            var storedToken = await _refreshTokenRepository.GetTokenAsync(model.RefreshToken);
            if (storedToken != null && !storedToken.IsRevoked && storedToken.Expires > DateTime.UtcNow)
            {
                var token = GenerateJwtToken(storedToken.Username);

                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim("Role", "Participant")
                },
                expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["AccessTokenExpiration"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RefreshRequest
    {
        public string RefreshToken { get; set; }
        public string Username { get; set; }
    }
}
