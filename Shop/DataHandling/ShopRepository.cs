using Shop.DataHandling;
using Shop.Logging;
using Shop.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public class ShopRepository
    {
        private ShopContext context;
        private ILogger logger;
        public ShopRepository (ShopContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
            #region Adding logging when collections are changed
            context.Invoices.CollectionChanged += CollectionChanged;
            context.ProductStates.CollectionChanged += CollectionChanged;
            #endregion
        }
        #region GetAll
        public ICollection<Client> GetAllClients() => context.Clients.ToList();
        public ICollection<Product> GetAllProducts() => context.Products.Values.ToList();
        public ICollection<ProductState> GetAllProductStates() => context.ProductStates.ToList();
        public ICollection<Invoice> GetAllInvoices() => context.Invoices.ToList();
        #endregion
        #region Get
        public Client GetClient(string id)
        {
            var client = context.Clients.Find(c => c.Id == id);
            if ( client is null)
            {
                throw new NotFoundException("Client not found");
            }
            else
            {
                return client;
            }
        }
        public Product GetProduct(string id)
        {
            try
            {
                var product = context.Products[id];
                return product;
            }
            catch(KeyNotFoundException)
            {
                throw new NotFoundException("Product not found");
            }
           
        }
        public Invoice GetInvoice(string id)
        {
            var invoice = context.Invoices.FirstOrDefault(i => i.Id == id);
            if ( invoice is null)
            {
                throw new NotFoundException("Invoice not found");
            }
            else
            {
                return invoice;
            }
        }
        public ProductState GetProductState(Product product) 
        {
            var productState = context.ProductStates.FirstOrDefault(p => p.Product.Id == product.Id);
            if (productState is null)
            {
                throw new NotFoundException("ProductState not found");
            }
            else
            {
                return productState;
            }
        }
        public ReportData GetReportData()
        {
            return context.ReportData;
        }
        #endregion
        #region Add
        public void Add(Client client)
        {
            if (context.Clients.Find(c => c.Id == client.Id) == null) // if no Client with given id
            {
                context.Clients.Add(client);
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            else
            {
                throw new DuplicateException("Cannot add client with identical id.");
            }
        }
        public void Add(Product product)
        {
            if (!context.Products.ContainsKey(product.Id)) // if no Product with given id
            {
                context.Products.Add(product.Id, product);
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            else
            {
                throw new DuplicateException("Cannot add product with identical id.");
            }
        }
        public void Add(Invoice invoice)
        {
            var isClientUnknown = ! context.Clients.Contains(invoice.Client);
            if (isClientUnknown)
            {
                context.Clients.Add(invoice.Client);
            }
            {
                context.Invoices.Add(invoice);
                context.ReportData.LastChangeTime = DateTime.Now;
            }
        }
        public void Add(ProductState productState)
        {
            if (context.ProductStates.FirstOrDefault(p => p.Product.Id == productState.Product.Id) == null) // if no ProductState describing the same product
            {
                context.ProductStates.Add(productState);
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            else
            {
                throw new DuplicateException("Cannot add productState describing the same product.");
            }
        }
        #endregion
        #region Delete
        public void Delete(Client client)
        {
            if (context.Clients.Contains(client) ) 
            {
                context.Clients.Remove(client);
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            else
            {
                throw new NotFoundException("Client not found.");
            }
        }
        public void Delete(Product product)
        {
            if (context.Products.ContainsKey(product.Id)) 
            {
                context.Products.Remove(product.Id);
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            else
            {
                throw new NotFoundException("Product not found.");
            }
        }
        public void Delete(Invoice invoice)
        {
            if (context.Invoices.Contains(invoice))
            {
                context.Invoices.Remove(invoice);
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            else
            {
                throw new NotFoundException("Invoice not found");
            }
        }
        public void Delete(ProductState productState)
        {
            if ( context.ProductStates.Contains(productState) ) 
            {
                context.ProductStates.Remove(productState);
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            else
            {
                throw new NotFoundException("Product State not found");
            }
        }
        #endregion
        #region Update
        public void Update(Client client, ClientData clientData)
        {
            if(clientData.IsFirstNameChanged == true)
            {
                client.FirstName = clientData.FirstName;
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            if (clientData.IsLastNameChanged == true)
            {
                client.LastName = clientData.LastName;
                context.ReportData.LastChangeTime = DateTime.Now;
            }

        }

        public void Update(Product product, ProductData productData)
        {
            if (productData.IsNameChanged == true)
            {
                product.Name = productData.Name;
                context.ReportData.LastChangeTime = DateTime.Now;
            }
        }

        public void Update(ProductState productState, ProductStateData productStateData)
        {
            if (productStateData.IsAmountChanged == true)
            {
                productState.Amount = productStateData.Amount;
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            if (productStateData.IsPriceNettoChanged == true)
            {
                productState.PriceNetto = productStateData.PriceNetto;
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            if (productStateData.IsTaxRateChanged == true)
            {
                productState.TaxRate = productStateData.TaxRate;
                context.ReportData.LastChangeTime = DateTime.Now;
            }
        }
        public void Update(Invoice invoice, InvoiceData invoiceData)
        {
            if (invoiceData.IsPurchaseTimeChanged == true)
            {
                invoice.PurchaseTime = invoiceData.PurchaseTime;
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            if (invoiceData.IsAmountChanged == true)
            {
                invoice.Amount = invoiceData.Amount;
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            if (invoiceData.IsTaxRateChanged == true)
            {
                invoice.TaxRate = invoiceData.TaxRate;
                context.ReportData.LastChangeTime = DateTime.Now;
            }
            if (invoiceData.IsPriceChanged == true)
            {
                invoice.Price = invoiceData.Price;
                context.ReportData.LastChangeTime = DateTime.Now;
            }
        }
        #endregion

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                StringBuilder itemsMessageBuilder = new StringBuilder();
                foreach(var item in e.NewItems)
                {
                    itemsMessageBuilder.Append(item.ToString() + " ");
                }
                logger.Log($"Added {itemsMessageBuilder} to {sender.ToString()}");
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                StringBuilder itemsMessageBuilder = new StringBuilder();
                foreach (var item in e.OldItems)
                {
                    itemsMessageBuilder.Append(item.ToString() + " ");
                }
                logger.Log($"Removed {itemsMessageBuilder} from {sender.ToString()}");
            }
        }
    }
}
