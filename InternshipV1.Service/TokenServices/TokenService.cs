using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Service.TokenServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration,
            UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));

        }
        public async Task<string> GenerateTokenForLogin(IdentityUser identityUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, identityUser.Email),
                new Claim("UserId", identityUser.Id),
                new Claim("UserName", identityUser.UserName),
            };

            var roles = await _userManager.GetRolesAsync(identityUser);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenDescribtor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Token:issuer"],
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescribtor);

            return tokenHandler.WriteToken(token);
        }
    }
}
