using KrzysztofSochaAPI.Services.Address.Dto;
using KrzysztofSochaAPI.Services.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Shop.Dto
{
    public class GetShopFullInformationsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AddressDto ShopAddress { get; set; }
        public GetUserDto Manager { get; set; }
       
    }
}
