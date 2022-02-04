using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Models
{
    public class Address
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Street { get; set; }
        [MaxLength(6)]
        public string PostalCode { get; set; }
        [MaxLength(5)]
        public string HouseNumber { get; set; }
        [MaxLength(5)]
        public string ApartamentNumber { get; set; }
        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
        public virtual Shop Shop { get; set; }

       
    }
}
