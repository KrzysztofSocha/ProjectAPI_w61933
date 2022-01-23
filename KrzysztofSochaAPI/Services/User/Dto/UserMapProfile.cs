using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.User.Dto
{
    public class UserMapProfile:Profile
    {
        public UserMapProfile()
        {
            CreateMap<RegisterUserDto, KrzysztofSochaAPI.Models.User>();
        }
    }
}
