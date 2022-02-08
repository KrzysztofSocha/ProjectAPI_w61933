using KrzysztofSochaAPI.Services.Address.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Order.Dto
{
    public class GetUserOrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
       
        public AddressDto AddressDelivery { get; set; }
    }
}
