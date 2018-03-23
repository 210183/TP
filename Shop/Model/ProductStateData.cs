using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model
{
    public class ProductStateData
    {
        public int Amount { get => Amount; set { Amount = value; IsAmountChanged = true; } }
        public bool IsAmountChanged { get; set; } = false;
        public decimal PriceNetto { get => PriceNetto; set { PriceNetto = value; IsPriceNettoChanged = true; } }
        public bool IsPriceNettoChanged { get; set; } = false;
        public Percentage TaxRate { get => TaxRate; set { TaxRate = value; IsTaxRateChanged = true; } }
        public bool IsTaxRateChanged { get; set; } = false;
    }
}
