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
    public class ClientTests
    {
        private readonly string clientFirstName = "Peter";
        private readonly string clientLastName = "Green";

        [TestMethod()]
        public void ClientConstructor_Test()
        {
            Client client = new Client(clientFirstName, clientLastName);
            Assert.AreEqual(client.FirstName, clientFirstName);
            Assert.AreEqual(client.LastName, clientLastName);
            Assert.IsNotNull(client.Id);
#if Debug
            Console.WriteLine(client.Id);
#endif
        }
    }
}