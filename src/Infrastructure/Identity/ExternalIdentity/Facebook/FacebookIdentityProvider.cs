using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Common.Models;
using SkipperAgency.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using SkipperAgency.Application.Common.Exceptions;

namespace SkipperAgency.Infrastructure.Identity.ExternalIdentity.Facebook
{
    public class FacebookIdentityProvider : IExternalIdentityProvider
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityService _identityService;
        private readonly IJwtServicecs _jwtFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpClient _httpClient;

        public FacebookIdentityProvider(UserManager<AppUser> userManager, IIdentityService identityService, IJwtServicecs jwtFactory, IConfiguration configuration, IHttpClient httpClient)
        {
            _userManager = userManager;
            _identityService = identityService;
            _jwtFactory = jwtFactory;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> ExternalLogin(string authToken)
        {
            var userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,picture&access_token={authToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            var user = await _userManager.FindByEmailAsync(userInfo.Email);
            if (user is null)
            {
                throw new NotFoundException("User", userInfo.Email);
            }
            var token = await _jwtFactory.GenerateEncodedToken(user);
            var roles = await _identityService.GetUserRoles(user.Email);
            return 
                new LoginResponse
                {
                    Token = token,
                    Username = user.UserName,
                    Role = roles.First().ToString(),
                    Id = user.Id,
                    UserPhotoUrl = user.UserPhotoUrl
                };
        }
    }
}
