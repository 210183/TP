using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop;
using Shop.Logging;
using Shop.Tests;

namespace ShopTests
{
    [TestClass]
    public class ShopServiceTest
    {
        private ShopContext context;
        private ShopRepository repository;
        private ShopService service;
        private ILogger logger;
        private Product product;

        [TestInitialize()]
        public void Initialize()
        {
            logger = new MockLogger();
            context = new ShopContext();
            var inserter = new ConstantDataInserter();
            inserter.InitializeContextWithData(context);
            repository = new ShopRepository(context, logger);
            service = new ShopService(repository,logger);

            product = context.Products.First().Value;
        }

        [TestMethod]
        public void ShopService_SellProduct_Test()
        {
            var timeBeforePurchase = DateTime.Now;
            var buyer = new Client("Albert", "King");
            var productState = context.ProductStates.FirstOrDefault(p => p.Product.Id == product.Id);
            int previousAmount = productState.Amount;
            service.SellProduct(buyer, product, 1);
            int currentAmount = context.ProductStates.FirstOrDefault(p => p.Product.Id == product.Id).Amount;
            Assert.AreEqual(previousAmount - 1, currentAmount);
            var newInvoice = context.Invoices.FirstOrDefault(i =>
                i.PurchaseTime >= timeBeforePurchase && // is there any new invoice 
                i.Client == buyer &&                    // with proper values
                i.Product == product &&
                i.Price == productState.PriceNetto &&
                i.TaxRate == productState.TaxRate
                );
            Assert.IsNotNull(newInvoice);
        }

        [TestMethod]
        public void ShopService_FindProductsByPrice_Test()
        {
            var chosenProducts = service.FindProductsWithPriceBetween(10000, 16000);
            Assert.AreEqual(chosenProducts.Count, 2);
        }
    }
}
