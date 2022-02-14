using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Shop.Dto
{
    public class ShopMapProfile:Profile
    {
        public ShopMapProfile()
        {
            CreateMap<CreateOrUpdateShopDto, Models.Shop>();
            CreateMap<Models.Shop, GetShopOutputDto>();
            CreateMap<Models.Shop, GetShopFullInformationsDto>()
                .ForMember(dest => dest.ShopAddress, opt => opt.MapFrom(src => src.Address));
        }
    }
}
