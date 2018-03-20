using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    class ProductState
    {
        public Product Product { get; } // what that would mean if you could change product described by that state ?
        public int Amount { get; set; }
        public decimal PriceNetto { get; set; }
        public Percentage TaxRate { get; set; }
        public ProductState(Product product, int amount, decimal priceNetto, Percentage taxRate)
        {
            Product = product;
            Amount = amount;
            PriceNetto = priceNetto;
            TaxRate = taxRate;
        }
    }
}
