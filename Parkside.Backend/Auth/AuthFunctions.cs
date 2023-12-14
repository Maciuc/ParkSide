using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
namespace Parkside.Backend.Auth
{
    public class AuthFunctions
    {
        private readonly IConfiguration _configuration;
        public AuthFunctions(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string? GetUserIdFromToken(string? token)
        {
            if (token == null)
            {
                return null;
            }

            token = token.Split(' ').LastOrDefault();

            if (token == null || token.Split('.').Length < 3)
            {
                return null;
            }

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            if (principal != null)
            {
                return principal.GetUserID();
            }
            else
            {
                return null;
            }
        }
    }
}