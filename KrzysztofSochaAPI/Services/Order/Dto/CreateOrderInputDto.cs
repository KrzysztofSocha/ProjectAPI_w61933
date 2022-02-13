using KrzysztofSochaAPI.Enums;
using KrzysztofSochaAPI.Services.Address.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Order.Dto
{
    public class CreateOrderInputDto
    {
        public DeliveryType DeliveryType { get; set; }
        public PaymentType PaymentType { get; set; }       
        public int TargetShopId { get; set; }
        public bool CustomDeliveryAddress { get; set; }
        public AddressDto DeliveryAddress { get; set; }
    }
}
