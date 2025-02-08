using IM_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IM_API.Utility
{
    public static class GenerateToken
    {
        public static string generateToken(Customer customer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConfigHelper.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("CustomerId", customer.CustomerId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Iss, ConfigHelper.JwtIssuer),
                    new Claim(JwtRegisteredClaimNames.Aud, ConfigHelper.JwtAudience)
                }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(ConfigHelper.JwtTokenExpirationDays)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                IssuedAt = DateTime.Now
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
