using AutoMapper;
using KrzysztofSochaAPI.Authorization;
using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Exceptions;
using KrzysztofSochaAPI.Services.User.Dto;
using KrzysztofSochaAPI.Services.UserContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly IUserContextAppService _userContextAppService;
        private readonly ILogger<UserAppService> _logger;
        
        public UserAppService(ProjectDbContext context,
          IMapper mapper,
          IPasswordHasher<Models.User> passwordHasher,
          AuthenticationSettings authenticationSettings,
          IAuthorizationService authorizationService,
          IUserContextAppService userContextAppService,
          ILogger<UserAppService> logger)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _authorizationService = authorizationService;
            _userContextAppService = userContextAppService;
            _logger = logger;
        }

        public void RegisterUser(RegisterUserDto input)
        {
            try
            {                
                var newUser = _mapper.Map<RegisterUserDto, Models.User>(input);
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

        public async Task<GetUserDto> UpdateUserAsync(int id, UpdateUserDto input)
        {

            var user = _context.Users.Include(x => x.Address).Where(x => x.Id == id && x.IsDeleted == false).Single();
            if (user is null)
                throw new NotFoundException("Nie istnieje taki użytkownik");

            var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextAppService.User,
               user,
               new ResourceOperationRequirement(ResourceOperation.Delete));
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Nie masz uprawnień do tej operacji.");
            }

            try
            {
                user.LastModificationTime = DateTime.Now;
                user.ModifierUserId = _userContextAppService.GetUserId;
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

                _context.SaveChanges();
                _logger.LogWarning($"Użytkownik o numerze Id: {_userContextAppService.GetUserId} pomyślnie edytował dane użytkownika o indyfikatorze: {id}");
                var output = _mapper.Map<Models.User, GetUserDto>(user);
                return output;


            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas edycji użytkownika: {ex.Message}");
            }
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var result = false;

            var user = _context.Users.FirstOrDefault(x => x.Id == id && x.IsDeleted == false && x.RoleId != 3);
            if (user is null)
                throw new NotFoundException("Nie istnieje taki użytkownik");

            var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextAppService.User,
                user,
                new ResourceOperationRequirement(ResourceOperation.Delete));
            if (!authorizationResult.Succeeded)
            {
                _logger.LogWarning($"Użytkownik o numerze Id: {_userContextAppService.GetUserId} próbował usunąć użytkownika o indyfikatorze: {id}");

                throw new ForbiddenException("Nie masz uprawnień do tej operacji.");
            }
            try
            {
                user.DeletionTime = DateTime.Now;
                user.DeletorUserId = _userContextAppService.GetUserId;
                user.IsDeleted = true;

                

                _context.SaveChanges();
                _logger.LogWarning($"Użytkownik o numerze Id: {_userContextAppService.GetUserId} pomyślnie usunął użytkownika o indyfikatorze: {id}");
                result = true;
                return result;

            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd podczas usuwania użytkownika: {ex.Message}");
            }
        }

        public Task<bool> ResetUserPassword(ResetPasswordDto input)
        {

            var user = _context.Users.FirstOrDefault(x => x.Id == input.UserId && x.IsDeleted == false);
            if (user is null)
                throw new NotFoundException("Nie istnieje taki użytkownik");
            try
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                user.LastModificationTime = DateTime.Now;
                user.ModifierUserId = _userContextAppService.GetUserId;
                _context.SaveChanges();
                _logger.LogWarning($"Hasło użytkownika o numerze Id: {input.UserId} zostało pomyślnie zmienione");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd poczas restaru hasła: {ex.Message}");
            }
        }

        public Task<bool> ChangeUserPassword(ChangePasswordDto input)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == _userContextAppService.GetUserId
            && x.IsDeleted == false);

            if (user is null)
                throw new NotFoundException("Nie istnieje taki użytkownik");
            var IsActualPassword= _passwordHasher.VerifyHashedPassword(user, user.Password,input.ActualPassword);
            if (IsActualPassword==PasswordVerificationResult.Failed)
                throw new Exception("Niepoprawne hasło");
            try
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                user.LastModificationTime = DateTime.Now;
                user.ModifierUserId = _userContextAppService.GetUserId;
                _context.SaveChanges();
                _logger.LogWarning($"Hasło użytkownika o numerze Id: {_userContextAppService.GetUserId} zostało pomyślnie zmienione");
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {

                throw new Exception($"Błąd poczas zmiany hasła: {ex.Message}");
            }
        }
    }
}
