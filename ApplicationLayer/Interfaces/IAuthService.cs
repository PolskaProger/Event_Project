using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(string firstName, string lastName, DateTime birthDate, string email, string password);
        Task<(string AccessToken, string RefreshToken)> LoginAsync(string email, string password);
        Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string token, string refreshToken);
    }
}
