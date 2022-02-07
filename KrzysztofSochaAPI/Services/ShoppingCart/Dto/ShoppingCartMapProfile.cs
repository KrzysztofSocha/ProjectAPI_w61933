using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.ShoppingCart.Dto
{
    public class ShoppingCartMapProfile:Profile
    {
        public ShoppingCartMapProfile()
        {
            CreateMap<AddShoppingCartItemDto, Models.ShoppingCartItem>();
            //CreateMap<Models.ShoppingCartItem, GetShopingCartOutputDto>();
            CreateMap<Models.Clothes, GetShopingCartClothesDto>();
        }
    }
}
