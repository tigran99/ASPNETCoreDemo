using IdentityServer.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace IdentityServer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private const string TokenSecret = "VerySecretInfoWhichShouldNotBePlacedHere";

        private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8);
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(ILogger<IdentityController> logger)
        {
            _logger = logger;
        }

        [HttpPost("token")]
        public IActionResult GenerateJwtToken(TokenGenerationRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityKey = Encoding.UTF8.GetBytes(TokenSecret);
            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, request.Email),
                new Claim(JwtRegisteredClaimNames.Email, request.Email),
                new Claim("userid", request.UserId.ToString())
            };

            foreach(var claim in request.CustomClaims)
            {
                var jsonElement = claim.Value;
                var valueType = jsonElement.ValueKind switch
                {
                     JsonValueKind.True => ClaimValueTypes.Boolean,
                     JsonValueKind.False => ClaimValueTypes.Boolean,
                     JsonValueKind.Number => ClaimValueTypes.Double,
                     _ => ClaimValueTypes.String
                };

                claims.Add(new Claim(claim.Key, claim.Value.ToString(), valueType));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifeTime),
                Issuer = "MyIssuer",
                Audience = "MyAudience",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            var jwt = tokenHandler.WriteToken(token);

            return Ok(jwt);
        }
    }
}
