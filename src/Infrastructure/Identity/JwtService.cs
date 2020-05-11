using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SkipperAgency.Infrastructure.Identity
{
    public class JwtService : IJwtServicecs
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public JwtService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> GenerateEncodedToken(AppUser user)
        {
            var utcNow = DateTime.UtcNow;
            var userRoles = await _userManager.GetRolesAsync(user);
            var userRolesString = string.Join(", ", userRoles.ToList());

            var claims = new Claim[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString()),
                        new Claim(JwtRegisteredClaimNames.Exp, utcNow.AddSeconds(Convert.ToDouble(_configuration["AppSettings:TokenExpiration"])).ToString()),
                        new Claim(ClaimTypes.Role, userRolesString),
                        new Claim("UserId", user.Id)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Secret"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(Convert.ToDouble(_configuration["AppSettings:TokenExpiration"])),
                audience: _configuration["AppSettings:Audience"],
                issuer: _configuration["AppSettings:Issuer"]
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
