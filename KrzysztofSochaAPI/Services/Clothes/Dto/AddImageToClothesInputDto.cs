using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Clothes.Dto
{
    public class AddImageToClothesInputDto
    {
        public int ClothesId { get; set; }
        public IFormFile Image { get; set; }
    }
}
