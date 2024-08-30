using EventsProject.Application.Interfaces;
using EventsProject.Domain.Entities;
using EventsProject.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<Participant> _participantRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(
            IRepository<Participant> participantRepository,
            IRepository<RefreshToken> refreshTokenRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _participantRepository = participantRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<string> RegisterAsync(string firstName, string lastName, DateTime birthDate, string email, string password)
        {
            var participant = new Participant
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                RegistrationDate = DateTime.UtcNow,
                Email = email,
                PasswordHash = _passwordHasher.HashPassword(password)
            };

            await _participantRepository.AddAsync(participant);
            return participant.Email;
        }

        public async Task<(string AccessToken, string RefreshToken)> LoginAsync(string email, string password)
        {
            var participant = await _participantRepository.GetSingleOrDefaultAsync(p => p.Email == email);

            if (participant == null || !_passwordHasher.VerifyPassword(participant.PasswordHash, password))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var accessToken = _tokenService.GenerateAccessToken(participant);
            var refreshToken = _tokenService.GenerateRefreshToken(participant.Email);
            await _refreshTokenRepository.AddAsync(refreshToken);

            return (accessToken, refreshToken.Token);
        }

        public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name;

            var storedRefreshToken = await _refreshTokenRepository.GetSingleOrDefaultAsync(r => r.Token == refreshToken);

            if (storedRefreshToken == null || storedRefreshToken.Expires < DateTime.UtcNow || storedRefreshToken.IsRevoked)
            {
                throw new SecurityTokenException("Invalid refresh token.");
            }

            storedRefreshToken.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(storedRefreshToken);

            var participant = await _participantRepository.GetSingleOrDefaultAsync(p => p.Email == username);
            var newAccessToken = _tokenService.GenerateAccessToken(participant);
            var newRefreshToken = _tokenService.GenerateRefreshToken(username);

            await _refreshTokenRepository.AddAsync(newRefreshToken);

            return (newAccessToken, newRefreshToken.Token);
        }
    }

}
