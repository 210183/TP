using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop;
using Shop.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Tests
{
    [TestClass()]
    public class ShopRepositoryTests
    {
        private ShopRepository repo;
        private ShopContext context;

        [TestInitialize()]
        public void Initialize()
        {
            ConsoleLogger logger = new ConsoleLogger();
            context = new ShopContext();
            ConstantDataInserter dataInserter = new ConstantDataInserter();
            dataInserter.InitializeContextWithData(context);
            repo = new ShopRepository(context, dataInserter, logger);

        }

        /*
        [TestMethod()]
        public void GetAllClientsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllProductsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllProductStatesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllInvoicesTest()
        {
            Assert.Fail();
        }
        */
        [TestMethod()]
        public void GetClientTest()
        {
            string clientName = "Buddy";

            var client = context.Clients.Find(c => c.FirstName == clientName);
            var newClient = repo.GetClient(client.Id);

            Assert.AreEqual(newClient.FirstName, clientName);

        }

        [TestMethod()]
        public void GetProductTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetInvoiceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetProductStateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetReportDataTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddTest3()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest3()
        {
            Assert.Fail();
        }

        /*
        [TestMethod()]
        public void CollectionChangedTest()
        {
            Assert.Fail();
        }
        */
    }
}