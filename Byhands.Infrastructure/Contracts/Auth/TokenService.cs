using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Byhands.Application.Utils;
using Byhands.Contract.Interfaces.Auth;
using Byhands.Domain.DTOs.Auth;
using Byhands.Domain.Entities.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Byhands.Infrastructure.Contracts.Auth;

public class TokenService : ITokenService
{
    private readonly JWTOptions jwtOptions;

    public TokenService(IOptions<JWTOptions> jwtOptions)
    {
        this.jwtOptions = jwtOptions.Value;
    }

    public AuthToken GenerateJWTToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim("Id", user.Id),
        };

        var secToken = new JwtSecurityToken(
            jwtOptions.Issuer,
            jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(secToken);

        return new AuthToken(token);
    }
}
