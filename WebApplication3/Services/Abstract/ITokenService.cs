using System.Security.Claims;
using WebApplication3.Entities;

namespace WebApplication3.Services.Abstract
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
