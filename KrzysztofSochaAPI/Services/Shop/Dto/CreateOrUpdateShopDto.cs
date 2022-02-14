using KrzysztofSochaAPI.Services.Address.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Shop.Dto
{
    public class CreateOrUpdateShopDto
    {       
        public string Name { get; set; }
        public AddressDto Address { get; set; }
    }
}
