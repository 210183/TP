using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop;
using Shop.DataHandling;
using Shop.Logging;
using Shop.Model;
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
            var logger = new MockLogger();
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


            Assert.AreEqual(invoice.PurchaseTime, newInvoice.PurchaseTime);
        }

        [TestMethod()]
        public void GetProductStateTest()
        {
            ProductState productState = context.ProductStates.First();
            ProductState newProductState = repo.GetProductState(productState.Product);
            Assert.AreEqual(productState.Product, newProductState.Product);
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
        [ExpectedException(typeof(DuplicateException))]
        public void AddProductStateTest()
        {
            repo.Add(productState);
            Assert.AreEqual(productState.Amount, repo.GetProductState(product).Amount);
            repo.Add(productState);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotFoundException))]
        public void DeleteClient_ShouldDeleteFromRepo_Test()
        {
            repo.Add(client);
            repo.Delete(client);
            repo.GetClient(client.Id);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotFoundException))]
        public void DeleteClientNotInRepo_Test()
        {
            var newClient = new Client("Vladimir", "Vladimirowicz");
            repo.Delete(newClient);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotFoundException))]
        public void DeleteProduct_ShouldDeleteFromRepo_Test()
        {
            repo.Add(product);
            repo.Delete(product);
            repo.GetClient(product.Id);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotFoundException))]
        public void DeleteProductNotInRepo_Test()
        {
            var newProduct = new Product("Dolmar");
            repo.Delete(newProduct);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotFoundException))]
        public void DeleteProductState_ShouldDeleteFromRepo_Test()
        {
            repo.Add(productState);
            repo.Delete(productState);
            repo.GetProductState(productState.Product);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotFoundException))]
        public void DeleteProductStateNotInRepo_Test()
        {
            var newProductState = new ProductState(product, 31, 141, new Percentage(0.21));
            repo.Delete(newProductState);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotFoundException))]
        public void DeleteInvoice_ShouldDeleteFromRepo_Test()
        {
            repo.Add(invoice);
            repo.Delete(invoice);
            repo.GetInvoice(invoice.Id);
        }

        [TestMethod()]
        [ExpectedException(typeof(NotFoundException))]
        public void DeleteInvoiceNotInRepo_Test()
        {
            var newInvoice = new Invoice(client, product);
            repo.Delete(newInvoice);
        }

        [TestMethod()]
        public void UpdateClientTest()
        {
            var clientData = new ClientData { LastName = "Kazakov" };
            Assert.IsTrue(clientData.LastName != client.LastName);
            repo.Update(client, clientData);
            Assert.IsTrue(clientData.LastName == client.LastName);

        }

        [TestMethod()]
        public void UpdateProductTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateProductStateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateInvoiceTest()
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