using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.Order.Dto
{
    public class PaymentDto
    {
        public int OrderId { get; set; }
        [MaxLength(6)]
        [MinLength(6)]        
        public string BlikCode { get; set; }
        public string BankName { get; set; }
    }
}
