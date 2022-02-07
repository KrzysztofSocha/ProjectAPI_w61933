using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.ShoppingCart.Dto
{
    public class GetShopingCartOutputDto
    {        
        public List<GetShopingCartClothesDto> ShoppingCartList { get; set; }
        public decimal TotalSumPrice { get; set; }
        public int NumberOfItem { get; set; }
    }
}
