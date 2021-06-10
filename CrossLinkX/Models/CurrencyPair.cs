using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossLinkX.Models
{
    public class CurrencyPair
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Amount { get; set; }
    }
}
