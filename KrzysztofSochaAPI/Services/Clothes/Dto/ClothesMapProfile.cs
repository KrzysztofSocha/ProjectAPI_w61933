using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Clothes.Dto
{
    public class ClothesMapProfile:Profile
    {
        public ClothesMapProfile()
        {
            CreateMap<AddClothesInputDto, Models.Clothes>();
            CreateMap<UpdateClothesInputDto, Models.Clothes>();
            CreateMap< Models.Clothes,GetClothesOutputDto>();
            CreateMap< Models.Image,GetImageOutputDto>();
        }
    }
}
