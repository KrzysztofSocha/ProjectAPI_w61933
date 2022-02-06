using KrzysztofSochaAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Clothes.Dto
{
    public class AddClothesInputDto
    {
        public string Description { get; set; }
        
        public SexType Sex { get; set; }
        
        public ClothCategory Category { get; set; }       
       
       
        public decimal Price { get; set; }
    }
}
