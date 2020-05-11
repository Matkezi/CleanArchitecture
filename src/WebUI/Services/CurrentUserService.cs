using Microsoft.AspNetCore.Http;
using SkipperAgency.Application.Common.Interfaces;
using System.Security.Claims;

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
