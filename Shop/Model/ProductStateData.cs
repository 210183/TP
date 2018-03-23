using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model
{
    class ProductStateData
    {
        public int Amount { get; set; }
        public decimal PriceNetto { get; set; }
        public Percentage TaxRate { get; set; }
    }
}
