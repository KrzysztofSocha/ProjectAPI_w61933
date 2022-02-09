using KrzysztofSochaAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Order.Dto
{
    public class ChangeOrderStatusInputDto
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
