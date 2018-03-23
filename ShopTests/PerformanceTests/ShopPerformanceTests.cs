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
        private ShopService shopService;

        [TestInitialize()]
        public void Initialize()
        {
            var logger = new MockLogger();
            context = new ShopContext();
            dataInserter = new RandomDataInserter();
            repo = new ShopRepository(context, logger);
            shopService = new ShopService(repo, logger);
        }

        [TestMethod]
        public void InsertThousandsOfData_ShouldLastMaxSecond_Test()
        {
            dataInserter = new RandomDataInserter(2000, 2000, 4000);
            dataInserter.InitializeContextWithData(context);
        }
        [TestMethod]
        public void InsertTensOfThousandsOfData_ShouldLastMaxTwoSecond_Test()
        {
            dataInserter = new RandomDataInserter(30000, 40000, 60000);
            dataInserter.InitializeContextWithData(context);
        }
        [TestMethod]
        public void InsertHundredsOfThousandsOfData_ShouldLastMaxTenSecond_Test()
        {
            dataInserter = new RandomDataInserter(500_000, 400_000, 200_000);
            dataInserter.InitializeContextWithData(context);
        }
        [TestMethod]
        public void BuyThousandsTimes_ShouldLastMaxTenSecond_Test()
        {
            dataInserter = new RandomDataInserter(1000, 100_00);
            dataInserter.InitializeContextWithData(context);
            Random randomizer = new Random();
            var products = new Product[shopService.GetAllProducts().Count];
            shopService.GetAllProducts().CopyTo(products, 0);
            Client buyer = new Client("Jimi", "Hendrix");
            Product productToBuy;
            for (int i = 0; i < 1000_0; i++)
            {
                productToBuy = products[randomizer.Next() % products.Length];
                try
                {
                    shopService.SellProduct(buyer, productToBuy, (randomizer.Next() % 2) + 1);
                }
                catch (NotEnoughProductException)
                {
                    //just skip to next try
                }
            }
        }
        [TestMethod]
        public void GetAllProductsCopyTo_Time_Test()
        {
            dataInserter = new RandomDataInserter(1000, 100_000);
            dataInserter.InitializeContextWithData(context);
            var products = new Product[shopService.GetAllProducts().Count];
            shopService.GetAllProducts().CopyTo(products, 0);
        }
    }
}
