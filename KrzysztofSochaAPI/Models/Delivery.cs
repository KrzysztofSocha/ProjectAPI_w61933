using KrzysztofSochaAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public DeliveryType Type { get; set; }
        public decimal Price { get; set; }
    }
}
