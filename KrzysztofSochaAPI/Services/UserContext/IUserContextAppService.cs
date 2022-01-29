using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.UserContext
{
    public interface IUserContextAppService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }
}
