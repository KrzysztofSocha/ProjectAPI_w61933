using KrzysztofSochaAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ReceivedTime { get; set; }
        public OrderStatus Status { get; set; }
        public int PurchaserId { get; set; }
        public virtual User Purchaser { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public virtual List<OrderClothes> OrderedClothes { get; set; }
       
        public decimal DeliveryPrice { get; set; }
        public decimal ClothesAmount { get; set; }
        public int DeliveryAddressId { get; set; }
        public virtual Address DeliveryAddress { get; set; }

    }
}
