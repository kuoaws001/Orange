using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Authority
{
    public class Authenticator
    {
        public static bool Authenticate(string clientId, string secret)
        {
            var app = AppRepository.GetApplicationByClientId(clientId);
            
            if (app == null) return false;

            return app.ClientId == clientId && app.Secret == secret;
        }

        public static string CreateToken(string clientId, DateTime expiresAt, string secretKey)
        {
            // algorithm
            // payload
            // signature

            var app = AppRepository.GetApplicationByClientId(clientId);

            var claims = new List<Claim>
            {
                new Claim("AppName", app?.Applicationname ?? string.Empty),
                new Claim("Read", (app?.Scopes ?? string.Empty).Contains("read") ? "true" : "false"),
                new Claim("Write", (app?.Scopes ?? string.Empty).Contains("write") ? "true" : "false")
            };

            var jwt = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256Signature),
                claims: claims,
                expires: expiresAt,
                notBefore: DateTime.UtcNow
            );


            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
