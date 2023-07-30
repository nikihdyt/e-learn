using ELearnAPI.EfCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ELearnAPI.Services
{
    public class JwtUtils
    {
        private readonly string _secretKey;

        public JwtUtils(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GenerateToken(User user)
        {
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(120);

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            string secretKey = configuration.GetSection("JwtSettings")["SecretKey"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
        };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expirationTime,
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
