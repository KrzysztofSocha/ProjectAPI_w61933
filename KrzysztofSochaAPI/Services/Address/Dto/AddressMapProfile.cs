using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Address.Dto
{
    public class AddressMapProfile:Profile
    {
        public AddressMapProfile()
        {
            CreateMap<AddressDto, KrzysztofSochaAPI.Models.Address>();
            CreateMap<Models.Address, AddressDto>();
        }
    }
}
