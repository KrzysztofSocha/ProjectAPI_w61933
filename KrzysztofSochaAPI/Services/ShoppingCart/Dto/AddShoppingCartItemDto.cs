using KrzysztofSochaAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.ShoppingCart.Dto
{
    public class AddShoppingCartItemDto
    {
        public int ClothesId { get; set; }       
        public SizeType Size { get; set; }
        public int Quantity { get; set; }
    }
}
