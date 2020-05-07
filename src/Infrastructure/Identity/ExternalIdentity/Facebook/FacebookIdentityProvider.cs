using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.ExternalLogins.Facebook;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity.ExternalIdentity
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

        public async Task<(Result result, LoginResponse loginResponse)> ExternalLogin(string authToken)
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
