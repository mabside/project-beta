using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Byhands.Application.Interfaces.Users;
using Byhands.Domain.DTOs.Auth;
using Byhands.Domain.Entities.Users;
using FastEndpoints.Security;
using Microsoft.IdentityModel.Tokens;

namespace Byhands.Infrastructure.Contracts.Auth;

public class AuthService : IAuthService
{
    public AuthToken GenerateJWTToken(User user, string secret)
    {
        var jwtToken = JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = "CraftedByHandForioSecretautentication";
                o.ExpireAt = DateTime.UtcNow.AddDays(1);
                o.User.Claims.Add(("id", user.Id));
            });

        return new AuthToken(jwtToken);
    }
}
