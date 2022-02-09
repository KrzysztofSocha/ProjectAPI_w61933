using KrzysztofSochaAPI.Enums;
using KrzysztofSochaAPI.Services.Address.Dto;
using KrzysztofSochaAPI.Services.ShoppingCart.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Order.Dto
{
    public class GetOrderOutputDto
    {
        public int Id { get; set; }
        public List<GetShopingCartClothesDto> Clothes { get; set; }
        public DateTime ReceivedTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreationTime { get; set; }
        public AddressDto DeliveryAddress { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int QuantityItems { get; set; }

        public decimal ClothesAmount { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
