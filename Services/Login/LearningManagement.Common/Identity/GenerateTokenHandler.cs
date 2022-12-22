using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearningManagement.Common.Identity
{
    public class GenerateTokenHandler
    {
        private readonly IConfiguration _configuration;

        public GenerateTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string userName, string role)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("jwt")["Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("jwt")["Issuer"],
                audience: _configuration.GetSection("jwt")["Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
