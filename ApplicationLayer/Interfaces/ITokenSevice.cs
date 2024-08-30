using EventsProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventsProject.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(Participant participant);
        RefreshToken GenerateRefreshToken(string username);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }

}
