using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public virtual User Manager { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
