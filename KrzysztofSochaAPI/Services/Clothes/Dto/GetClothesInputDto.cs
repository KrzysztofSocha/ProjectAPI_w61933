using KrzysztofSochaAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Clothes.Dto
{
    public class GetClothesInputDto
    {
        public SexType Sex { get; set; }
        
        public ClothCategory Category { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
        public int StartIndex { get; set; }
        public int MaxResultCount { get; set; }
        public SortingClothesType SortingType { get; set; }

    }
}
