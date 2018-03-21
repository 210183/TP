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
    public class ProductTests
    {
        private readonly string productName = "Fender";

        [TestMethod()]
        public void ProductTest()
        {
            Product product = new Product(productName);

            Assert.AreEqual(product.Name, productName);
            Assert.IsNotNull(product.Id);

        }

    }
}