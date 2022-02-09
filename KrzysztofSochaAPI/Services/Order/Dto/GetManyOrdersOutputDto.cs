using KrzysztofSochaAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Order.Dto
{
    public class GetManyOrdersOutputDto
    {
        public int Id { get; set; }
        
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreationTime { get; set; }
       
       
        public int QuantityItems { get; set; }

        public decimal ClothesAmount { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
