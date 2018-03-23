using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model
{
    class InvoiceData
    {
        public DateTime PurchaseTime { get; set; }      
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Percentage TaxRate { get; set; }
    }
}
