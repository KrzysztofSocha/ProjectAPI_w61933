using KrzysztofSochaAPI.Services.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.User
{
    public interface IUserAppService
    {
        void RegisterUser(RegisterUserDto input);
        string GenerateJwt(LoginUserDto input);
    }
}
