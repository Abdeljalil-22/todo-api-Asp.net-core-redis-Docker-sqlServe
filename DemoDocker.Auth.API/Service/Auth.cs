using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoDocker.Auth.API.Service
{
    public class Auth
    {
        private readonly IConfiguration _configuration;

        public Auth(IConfiguration configuration)
        {
            _configuration = configuration;
        }

     



     

        public string GenerateToken( string email)
        {
            var s = _configuration.GetValue<string>("AppSettings:Secret");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(s);
            var claims = new List<Claim>();

            claims.Add(new Claim(type: "email", value: email));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var generatedToken = tokenHandler.WriteToken(token);
            return generatedToken;
        }

    }
}
