using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Byhands.Domain.DTOs.Auth;
using Byhands.Domain.Entities.Users;

namespace Byhands.Application.Interfaces.Users;

public interface IAuthService
{
    AuthToken GenerateJWTToken(User user, string secret);
}
