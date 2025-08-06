using Microsoft.AspNetCore.Http;
using Rento.Application.Common.Interfaces;
using System.Security.Claims;

namespace Rento.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int UserId { get; }
        public string Role { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                var roleClaim = user.FindFirst(ClaimTypes.Role);

                UserId = int.TryParse(userIdClaim?.Value, out var id) ? id : 0;
                Role = roleClaim?.Value ?? string.Empty;
            }
        }
    }
}
