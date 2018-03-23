using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model
{
    public class InvoiceData
    {
        public DateTime PurchaseTime {get => PurchaseTime; set { PurchaseTime = value; IsPurchaseTimeChanged = true;} }
        public bool IsPurchaseTimeChanged { get; set; } = false;
        public int Amount { get => Amount; set { Amount = value; IsAmountChanged = true; } }
        public bool IsAmountChanged { get; set; } = false;
        public decimal Price { get => Price; set { Price = value; IsPriceChanged = true; } }
        public bool IsPriceChanged { get; set; } = false;
        public Percentage TaxRate { get => TaxRate; set { TaxRate = value; IsTaxRateChanged = true; } }
        public bool IsTaxRateChanged { get; set; } = false;
    }
}
