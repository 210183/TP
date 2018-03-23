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
    public class InvoiceTests
    {
        private readonly string clientFirstName = "Peter";
        private readonly string clientLastName = "Green";
        private readonly string productName = "Fender";

        [TestMethod()]
        public void InvoiceConstructor_Test()
        {
            DateTime testTime = DateTime.Now;
            

            Invoice invoice = new Invoice(
                new Client(clientFirstName,clientLastName), 
                new Product(productName),
                1,
                (decimal)123.123,
                new Percentage(21));

            Assert.IsTrue(testTime <= invoice.PurchaseTime);
            Assert.IsTrue(testTime.AddSeconds(1) > invoice.PurchaseTime);
            Assert.IsNotNull(invoice.Id);
            Assert.IsNotNull(invoice.Client);
            Assert.IsNotNull(invoice.Product);
            
        }


    }
}