using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Models
{
    public class ShoppingCartItem
    {
        
        public int UserId  { get; set; }
        public virtual User User  { get; set; }
        public int ClothesId { get; set; } 
        public virtual Clothes Clothes { get; set; }
    }
}
