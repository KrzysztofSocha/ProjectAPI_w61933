using KrzysztofSochaAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentType Type { get; set; }
        public decimal Amount { get; set; }
        public virtual Order Order { get; set; }

    }
}
