using KrzysztofSochaAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.ShoppingCart.Dto
{
    public class GetShopingCartClothesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public SizeType Size { get; set; }
        public int Quantity { get; set; }


        public decimal SumPrice { get; set; }
    }
}
