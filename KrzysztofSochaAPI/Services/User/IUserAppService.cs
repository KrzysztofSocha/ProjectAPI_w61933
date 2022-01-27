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
        Task<GetUserDto> UpdateUser(int id,UpdateUserDto input);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ResetUserPassword(ResetPasswordDto input, int adminId);
    }
}
