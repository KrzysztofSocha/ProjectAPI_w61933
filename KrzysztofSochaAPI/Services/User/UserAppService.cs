using AutoMapper;
using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Services.User.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.User
{
    public class UserAppService : IUserAppService
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Models.User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserAppService(ProjectDbContext context,
          IMapper mapper,
          IPasswordHasher<KrzysztofSochaAPI.Models.User> passwordHasher,
          AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public void RegisterUser(RegisterUserDto input)
        {
            try
            {
                //var checkEmail = _context.Users.FirstOrDefault(x => x.Email == input.Email);
                //if(checkEmail is not null)                
                //    throw new Exception($"Użytkownik o podanym mailu jest już zarejstrowany.");
                
                //if(input.CreatePassword != input.ConfrimPassword)
                //    throw new Exception($"Podane hasła różnią się.");
                var newUser = _mapper.Map<RegisterUserDto, KrzysztofSochaAPI.Models.User>(input);
                newUser.CreationTime = DateTime.Now;
                newUser.Password = _passwordHasher.HashPassword(newUser, input.CreatePassword);
                _context.Users.Add(newUser);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas dodawnia użytkownika: {ex.Message}");
            }


        }
        public string GenerateJwt(LoginUserDto input)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == input.Email);

            if (user is null)
            {
                throw new Exception("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, input.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.Surname}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString("yyyy-MM-dd")),
                new Claim(ClaimTypes.MobilePhone, user.Phone)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }
    }
}
