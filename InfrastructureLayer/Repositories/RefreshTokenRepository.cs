using EventsProject.Domain.Entities;
using EventsProject.Domain.Interfaces;
using EventsProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetTokenAsync(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task AddTokenAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task RevokeTokenAsync(string token)
        {
            var refreshToken = await GetTokenAsync(token);
            if (refreshToken != null)
            {
                refreshToken.IsRevoked = true;
                refreshToken.Revoked = DateTime.UtcNow;
                _context.RefreshTokens.Update(refreshToken);
                await _context.SaveChangesAsync();
            }
        }
    }
}
