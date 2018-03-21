using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Tests
{

    [TestClass()]
    public class ProductStateTests
    {
        private readonly string name = "Fender";
        private readonly int amount = 2;
        private readonly decimal priceNetto = 10;
        private readonly Percentage taxRate = new Percentage(0.2);

        [TestMethod()]
        public void ProductStateTest()
        {
            Product product = new Product(name);
            ProductState productState = new ProductState(product, amount, priceNetto, taxRate);

            Assert.IsNotNull(product);
            Assert.AreEqual(productState.Amount, amount);
            Assert.AreEqual(productState.PriceNetto, priceNetto);
            Assert.AreEqual(productState.TaxRate, taxRate);

        }


    }
}