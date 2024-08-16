using EventsProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetTokenAsync(string token);
        Task AddTokenAsync(RefreshToken token);
        Task RevokeTokenAsync(string token);
    }
}
