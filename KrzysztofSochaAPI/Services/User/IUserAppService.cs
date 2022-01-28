using KrzysztofSochaAPI.Services.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.User
{
    public interface IUserAppService
    {
        void RegisterUser(RegisterUserDto input);
        string GenerateJwt(LoginUserDto input);
        Task<GetUserDto> UpdateUser(int id,UpdateUserDto input);
        Task<bool> DeleteUser(int id, ClaimsPrincipal user, int deleterId);
        Task<bool> ResetUserPassword(ResetPasswordDto input, int adminId);
    }
}
