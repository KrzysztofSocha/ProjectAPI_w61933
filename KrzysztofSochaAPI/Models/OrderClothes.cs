using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Models
{
    public class OrderClothes
    {
        public int OrderId { get; set; }
        public virtual Order Order{ get; set; }
        public int OrderedClothesId { get; set; }
        public virtual Clothes OrderedClothes { get; set; }
    }
}
