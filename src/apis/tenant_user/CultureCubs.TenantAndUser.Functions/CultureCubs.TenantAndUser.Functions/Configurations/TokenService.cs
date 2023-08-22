using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CultureCubs.TenantAndUser.Functions.Configurations
{
    public class TokenService : ITokenService
    {
       

        public string GenerateToken(string userName)
        {
            var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7xJ8WLF67zz/IfG1bkyDnp+45SNcSHUNtmJbEiRQQ4A="));
            var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "UserManagementAPI",
                //audience: configuration["Jwt:Audience"],
                claims: new[] { new Claim(ClaimTypes.Name, userName) },
                expires: DateTime.UtcNow.AddHours(1), // Set token expiration
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
