using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
