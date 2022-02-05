﻿using KrzysztofSochaAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Models
{
    public class Clothes
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public SexType Sex { get; set; }
        public ClothCategory Category { get; set; }
        public bool IsAvailability { get; set; }
        public decimal Price { get; set; }
        public virtual List<Image> Images { get; set; }
        public virtual List<OrderClothes> Orders { get; set; }

        public List<ShoppingCartItem> UserBuyers { get; set; }

    }
}
