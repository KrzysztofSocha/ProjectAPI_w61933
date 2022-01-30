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
        Task<GetUserDto> UpdateUserAsync(int id,UpdateUserDto input);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ResetUserPassword(ResetPasswordDto input);
        Task<bool> ChangeUserPassword(ChangePasswordDto input);
    }
}
