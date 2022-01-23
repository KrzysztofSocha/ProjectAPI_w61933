using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Address.Dto
{
    public class AddressDto
    {
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Street { get; set; }
        [MaxLength(6)]
        public string PostalCode { get; set; }
        [MaxLength(5)]
        public string HouseNumber { get; set; }
        [MaxLength(5)]
        public string? ApartamentNumber { get; set; }
    }
}
