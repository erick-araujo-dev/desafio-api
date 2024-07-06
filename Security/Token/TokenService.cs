using Microsoft.IdentityModel.Tokens;
using SorteOnlineDesafio.Application.Interfaces;
using SorteOnlineDesafio.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SorteOnlineDesafio.Security.Token
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        private readonly int _expirationMinutes;

        public TokenService(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:SecretKey"];
            _expirationMinutes = int.Parse(configuration["Jwt:ExpirationMinutes"]);
        }

        public string GenerateToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Nome.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UsuarioId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)

                }),
                Expires = DateTime.UtcNow.AddMinutes(_expirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
