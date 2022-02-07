using KrzysztofSochaAPI.Enums;
using KrzysztofSochaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Clothes.Dto
{
    public class GetClothesOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
        public SexType Sex { get; set; }
       
        public ClothCategory Category { get; set; }      
        
       
        public decimal Price { get; set; }
        public virtual List<GetImageOutputDto> Images { get; set; }
    }
}
