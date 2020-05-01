using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.ExternalLogins.Facebook;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class ExternalIdentityService : IExternalIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityService _identityService;
        private readonly IJwtFactory _jwtFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpClient _httpClient;

        public ExternalIdentityService(UserManager<AppUser> userManager, IIdentityService identityService, IJwtFactory jwtFactory, IConfiguration configuration, IHttpClient httpClient)
        {
            _userManager = userManager;
            _identityService = identityService;
            _jwtFactory = jwtFactory;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<(Result, LoginResponse)> FacebookLogin(string authToken)
        {

            var userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,picture&access_token={authToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            AppUser user = await _userManager.FindByNameAsync(userInfo.Email);
            if (user is null)
            {
                string errorMessage = $"Login failed. User: {userInfo.Email}";
                return (Result.Failure(errorMessage), null);
            }
            var token = await _jwtFactory.GenerateEncodedToken(user);
            var roles = await _identityService.GetUserRoles(user.Email);
            return (Result.Success(),
                new LoginResponse
                {
                    Token = token,
                    Username = user.UserName,
                    Role = roles.First().ToString(),
                    Id = user.Id,
                    UserPhotoUrl = user.UserPhotoUrl
                });
        }
    }
}
