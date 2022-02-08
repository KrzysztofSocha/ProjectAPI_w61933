using KrzysztofSochaAPI.Services.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Order.Dto
{
    public class CreateOrderOutputDto
    {
        public int Id { get; set; }
        
        public decimal ClothesAmount { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public GetUserOrderDto User { get; set; }
    }
}
