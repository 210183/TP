using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop;
using Shop.Logging;
using Shop.Tests;

namespace ShopTests.PerformanceTests
{
    [TestClass]
    public class ShopPerformanceTests
    {
        private ShopRepository repo;
        private ShopContext context;
        private IDataInserter dataInserter;

        [TestInitialize()]
        public void Initialize()
        {
            var logger = new MockLogger();
            context = new ShopContext();
            dataInserter = new RandomDataInserter();
            repo = new ShopRepository(context, logger);
        }

        [TestMethod]
        [Timeout(1000)]
        public void InsertThousandsOfData_ShouldLastMaxSecond_Test()
        {
            dataInserter = new RandomDataInserter(2000, 2000, 4000);
            dataInserter.InitializeContextWithData(context);
        }
        [TestMethod]
        [Timeout(2000)]
        public void InsertTensOfThousandsOfData_ShouldLastMaxTwoSecond_Test()
        {
            dataInserter = new RandomDataInserter(30000, 40000, 60000);
            dataInserter.InitializeContextWithData(context);
        }
        [TestMethod]
        [Timeout(10000)]
        public void InsertHundredsOfThousandsOfData_ShouldLastMaxTwoSecond_Test()
        {
            dataInserter = new RandomDataInserter(300000, 400000, 600000);
            dataInserter.InitializeContextWithData(context);
        }
    }
}
