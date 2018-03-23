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
            taxRate = new Percentage(20);

            client = new Client("John", "Doe");
            product = new Product("Chicken");
            invoice = new Invoice(client, product, 1, (decimal)123.123, new Percentage(21));
            productState = new ProductState(product, amount, priceNetto, taxRate);
        }

        
        [TestMethod()]
        public void GetAllClientsTest()
        {
            Assert.AreEqual(context.Clients.Count, repo.GetAllClients().Count);
        }

        [TestMethod()]
        public void GetAllProductsTest()
        {
            Assert.AreEqual(context.Products.Count, repo.GetAllProducts().Count);
        }

        [TestMethod()]
        public void GetAllProductStatesTest()
        {
            Assert.AreEqual(context.ProductStates.Count, repo.GetAllProductStates().Count);
        }

        [TestMethod()]
        public void GetAllInvoicesTest()
        {
            Assert.AreEqual(context.Invoices.Count, repo.GetAllInvoices().Count);
        }
        
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
            var newProductState = new ProductState(product, 31, 141, new Percentage(21));
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
            var newInvoice = new Invoice(client, product, 1, (decimal)123.123, new Percentage(21));
            repo.Delete(newInvoice);
        }

        [TestMethod()]
          public void UpdateClientTest()
        {
            ClientData clientData = new ClientData { LastName = "Kazakov" };
            Assert.IsTrue(clientData.LastName != client.LastName);
            repo.Update(client, clientData);
            Assert.IsTrue(clientData.LastName == client.LastName);
            Assert.IsFalse(clientData.IsFirstNameChanged);

        }

        [TestMethod()]
        public void UpdateProductTest()
        {
            var productData = new ProductData { Name = "Banana" };
            Assert.IsTrue(productData.Name != product.Name);
            repo.Update(product, productData);
            Assert.IsTrue(productData.Name == product.Name);
        }

        [TestMethod()]
        public void UpdateProductStateTest()
        {
            var productStateData = new ProductStateData { Amount = 15, PriceNetto = 10 };
            Assert.IsTrue(productStateData.Amount != productState.Amount);
            Assert.IsTrue(productStateData.PriceNetto != productState.PriceNetto);
           repo.Update(productState, productStateData);
            Assert.IsTrue(productStateData.Amount == productState.Amount);
            Assert.IsTrue(productStateData.PriceNetto == productState.PriceNetto);
            Assert.IsFalse(productStateData.IsTaxRateChanged);
        }

        [TestMethod()]
        public void UpdateInvoiceTest()
        {
            var invoiceData = new InvoiceData { Amount = 15, TaxRate = new Percentage(10) };
            Assert.IsTrue(invoiceData.Amount != invoice.Amount);
            Assert.IsTrue(invoiceData.TaxRate!= invoice.TaxRate);
            repo.Update(invoice, invoiceData);
            Assert.IsTrue(invoiceData.Amount == invoice.Amount);
            Assert.IsTrue(invoiceData.TaxRate == invoice.TaxRate);
            Assert.IsFalse(invoiceData.IsPriceChanged);
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