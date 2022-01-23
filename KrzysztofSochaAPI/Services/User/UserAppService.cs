using AutoMapper;
using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Services.User.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.User
{
    public class UserAppService : IUserAppService
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Models.User> _passwordHasher;

        public UserAppService(ProjectDbContext context,
          IMapper mapper,
          IPasswordHasher<KrzysztofSochaAPI.Models.User> passwordHasher)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public void RegisterUser(RegisterUserDto input)
        {
            try
            {
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
    }
}
