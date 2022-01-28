using AutoMapper;
using KrzysztofSochaAPI.Authorization;
using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Services.User.Dto;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IAuthorizationService _authorizationService;

        public UserAppService(ProjectDbContext context,
          IMapper mapper,
          IPasswordHasher<KrzysztofSochaAPI.Models.User> passwordHasher,
          AuthenticationSettings authenticationSettings,
          IAuthorizationService authorizationService)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _authorizationService = authorizationService;
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
                .FirstOrDefault(u => u.Email == input.Email && u.IsDeleted ==false);

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

        public Task<GetUserDto> UpdateUser(int id ,UpdateUserDto input)
        {
            try
            {
                var user=_context.Users.Include(x=>x.Address).Where(x=>x.Id==id &&x.IsDeleted==false).Single();
                if (user is null)
                    throw new Exception("Nie istnieje taki użytkownik");
                //user = _mapper.Map<UpdateUserDto, KrzysztofSochaAPI.Models.User>(input, user);
                user.LastModificationTime = DateTime.Now;
                //tymczasowo 
                //TODO aby pobierało aktualnie zalogowanego
                user.ModifierUserId = id;
                #region searchChanges
                if (!string.IsNullOrEmpty(input.Name))
                    user.Name = input.Name; 
                if (!string.IsNullOrEmpty(input.Surname))
                    user.Surname = input.Surname;
                if (!string.IsNullOrEmpty(input.Email))
                    user.Email = input.Email;
                if (!string.IsNullOrEmpty(input.Phone))
                    user.Phone = input.Phone;
                if (!string.IsNullOrEmpty(input.Address.HouseNumber))
                    user.Address.HouseNumber = input.Address.HouseNumber;
                if (!string.IsNullOrEmpty(input.Address.ApartamentNumber))
                    user.Address.ApartamentNumber = input.Address.ApartamentNumber;
                if (!string.IsNullOrEmpty(input.Address.City))
                    user.Address.City = input.Address.City;
                if (!string.IsNullOrEmpty(input.Address.PostalCode))
                    user.Address.PostalCode = input.Address.PostalCode;
                if (input.DateOfBirth != DateTime.MinValue)
                    user.DateOfBirth = input.DateOfBirth;
                #endregion
                //_context.Users.Update(user);
                _context.SaveChanges();
                var output = _mapper.Map<Models.User, GetUserDto>(user);
                return Task.FromResult(output);


            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas edycji użytkownika: {ex.Message}");
            }
        }
        public  Task<bool> DeleteUser(int id, ClaimsPrincipal deleter, int deleterId)
        {
            var result = false;
            try
            {
                var user =  _context.Users.FirstOrDefault(x => x.Id == id && x.IsDeleted==false && x.RoleId!=3);
                if (user is null)
                    throw new Exception("Nie istnieje taki użytkownik");
                var authorizationResult=_authorizationService.AuthorizeAsync(deleter,
                    user, 
                    new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
                if (!authorizationResult.Succeeded)
                {
                    throw new Exception("Nie masz uprawnień do tej operacji.");
                }
                user.DeletionTime = DateTime.Now;
               
                //TODO dodać logera
                user.DeletorUserId = deleterId;
                user.IsDeleted = true;
                
                
                _context.SaveChanges();
                result = true;
                return Task.FromResult(result);


            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas usuwania użytkownika: {ex.Message}");
            }
        }

        public Task<bool> ResetUserPassword(ResetPasswordDto input, int adminId)
        {
            try
            {               
                var user = _context.Users.FirstOrDefault(x => x.Id == input.UserId && x.IsDeleted == false);
                if (user is null)
                    throw new Exception("Nie istnieje taki użytkownik");
                user.Password= _passwordHasher.HashPassword(user, input.NewPassword);
                user.LastModificationTime = DateTime.Now;
                user.ModifierUserId = adminId;
                _context.SaveChanges();
                return Task.FromResult( true);
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd poczas restaru hasła: {ex.Message}");
            }
        }
    }
}
