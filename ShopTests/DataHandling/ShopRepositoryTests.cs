using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop;
using Shop.DataHandling;
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
        private Client client;
        private Product product;
        private Invoice invoice;
        private ProductState productState;

        private int amount = 2;
        private decimal priceNetto = 2;
        private Percentage taxRate;

        [TestInitialize()]
        public void Initialize()
        {
            ConsoleLogger logger = new ConsoleLogger();
            context = new ShopContext();
            ConstantDataInserter dataInserter = new ConstantDataInserter();
            dataInserter.InitializeContextWithData(context);
            repo = new ShopRepository(context, logger);
            taxRate = new Percentage(0.2);

            client = new Client("John", "Doe");
            product = new Product("Chicken");
            invoice = new Invoice(client, product);
            productState = new ProductState(product, amount, priceNetto, taxRate);
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
            Product product = context.Products.First().Value;
            Product newProduct = repo.GetProduct(product.Id);

            Assert.AreEqual(product.Name, newProduct.Name);

        }

        [TestMethod()]
        public void GetInvoiceTest()
        {
            Invoice invoice = context.Invoices.First();
            Invoice newInvoice = repo.GetInvoice(invoice.Id);


            Assert.AreEqual(invoice.PurchaseTime, newInvoice.PurchaseTime );
        }

        [TestMethod()]
        public void GetProductStateTest()
        {
            ProductState productState = context.ProductStates.First();
            ProductState newProductState = repo.GetProductState(productState.Product);
            Assert.AreEqual(productState.Product, newProductState.Product );
        }

        [TestMethod()]
        [ExpectedException(typeof(DuplicateException))]
        public void AddClientTest()
        {    
            repo.Add(client);
            Assert.AreEqual(client.FirstName, repo.GetClient(client.Id).FirstName);
            repo.Add(client);
        }

        [TestMethod()]
        [ExpectedException(typeof(DuplicateException))]
        public void AddProductTest()
        {
            repo.Add(product);
            Assert.AreEqual(product.Name, repo.GetProduct(product.Id).Name);
            repo.Add(product);
        }

        [TestMethod()]
        public void AddInvoiceTest()
        {
            repo.Add(invoice);
            Assert.AreEqual(invoice.Product, repo.GetInvoice(invoice.Id).Product);
           
        }

        [TestMethod()]
        public void AddProductStateTest()
        {
            repo.Add(productState);
            Assert.AreEqual(productState.Amount, repo.GetProductState(product).Amount);
            try
            {
                repo.Add(productState);
            }
            catch (DuplicateException)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteClientTest()
        {
            repo.Add(client);
            repo.Delete(client);

            int passedCounter = 0;

            try
            {
                repo.GetClient(client.Id);
            }
            catch (NotFoundException)
            {
                passedCounter++;
            }
            try
            {
                repo.Delete(client);
            }
            catch (NotFoundException)
            {
                passedCounter++;
            }
            if(passedCounter==2)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void DeleteProductTest()
        {
            repo.Add(product);
            repo.Delete(product);

            int passedCounter = 0;

            try
            {
                repo.GetProduct(product.Id);
            }
            catch (NotFoundException)
            {
                passedCounter++;
            }
            try
            {
                repo.Delete(product);
            }
            catch (NotFoundException)
            {
                passedCounter++;
            }
           
            if (passedCounter == 2)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void DeleteProductStateTest()
        {  
            repo.Add(productState);
            repo.Delete(productState);

            int passedCounter = 0;

            try
            {
                repo.GetProductState(product);
            }
            catch (NotFoundException)
            {
                passedCounter++;
            }
            try
            {
                repo.Delete(productState);
            }
            catch (NotFoundException)
            {
                passedCounter++;
            }

            if (passedCounter == 2)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void DeleteInvoiceTest()
        {
            repo.Add(invoice);
            repo.Delete(invoice);

            int passedCounter = 0;

            try
            {
                repo.GetInvoice(invoice.Id);
            }
            catch (NotFoundException)
            {
                passedCounter++;
            }
            try
            {
                repo.Delete(invoice);
            }
            catch (NotFoundException)
            {
                passedCounter++;
            }

            if (passedCounter == 2)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
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