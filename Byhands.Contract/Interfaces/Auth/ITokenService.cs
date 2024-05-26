using Byhands.Domain.DTOs.Auth;
using Byhands.Domain.Entities.Users;

namespace Byhands.Contract.Interfaces.Auth;

public interface ITokenService
{
    AuthToken GenerateJWTToken(User user, string secret, string issuer);
}
