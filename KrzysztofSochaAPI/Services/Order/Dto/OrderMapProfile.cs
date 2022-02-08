using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Order.Dto
{
    public class OrderMapProfile:Profile
    {
        public OrderMapProfile()
        {
            CreateMap<Models.ShoppingCartItem, Models.OrderClothes>();
            CreateMap<Models.User, GetUserOrderDto>();
        }
    }
}
