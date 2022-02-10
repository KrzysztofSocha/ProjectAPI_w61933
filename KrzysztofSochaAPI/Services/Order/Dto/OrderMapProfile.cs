using AutoMapper;
using KrzysztofSochaAPI.Services.ShoppingCart.Dto;
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
            CreateMap<Models.ShoppingCartItem, Models.OrderClothes>()
                .ForMember(dest => dest.OrderedClothesId, opt => opt.MapFrom(src => src.ClothesId));
            CreateMap<Models.User, GetUserOrderDto>();
            CreateMap<Models.Order, GetOrderOutputDto>()
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.Payment.PaymentDate)) 
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.Payment.Status)) 
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status)) 
                .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.Payment.Type));
            CreateMap<Models.OrderClothes, GetShopingCartClothesDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderedClothesId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OrderedClothes.Name))
                .ForMember(dest => dest.SumPrice, opt => opt.MapFrom(src => src.Quantity*src.OrderedClothes.Price));
            CreateMap<Models.Order, GetManyOrdersOutputDto>()
                .ForMember(dest => dest.DeliveryPrice, opt => opt.MapFrom(src => src.FreeDelivery? 0 : src.Delivery.Price))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status));
               
        }
    }
}
