using Shop.DataHandling;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    class ShopRepository
    {
        private ShopContext context;

        public ShopRepository (ShopContext context, IDataInserter inserter)
        {
            this.context = context;
            inserter.InitializeContextWithData(context);
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
            var product = context.Products[id];
            if (product is null)
            {
                throw new NotFoundException("Product not found");
            }
            else
            {
                return product;
            }
        }
        public Invoice GetInvoice(string id)
        {
            var invoice = context.Invoices.Where(i => i.Id == id).FirstOrDefault();
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
            var productState = context.ProductStates.Where(p => p.Product.Id == product.Id).FirstOrDefault();
            if (productState is null)
            {
                throw new NotFoundException("ProductState not found");
            }
            else
            {
                return productState;
            }
        }
        #endregion
        #region Add
        public void Add(Client client)
        {
            if (context.Clients.Find(c => c.Id == client.Id) == null) // if no Client with given id
            {
                context.Clients.Add(client);
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
            context.Invoices.Add(invoice);
        }
        public void Add(ProductState productState)
        {
            if (context.ProductStates.Where(p => p.Product.Id == productState.Product.Id).FirstOrDefault() == null) // if no ProductState describing the same product
            {
                context.ProductStates.Add(productState);
            }
            else
            {
                throw new DuplicateException("Cannot add productState  describing the same product.");
            }
        }
        #endregion
        #region Delete
        public void Delete(Client client)
        {
            if (context.Clients.Contains(client) ) 
            {
                context.Clients.Remove(client);
            }
            else
            {
                throw new NotFoundException("Client not found.");
            }
        }
        public void Delete(Product product)
        {
            if (!context.Products.ContainsKey(product.Id)) 
            {
                context.Products.Remove(product.Id);
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
            }
            else
            {
                throw new NotFoundException("Product State not found");
            }
        }
        #endregion
        /// <summary>
        /// Reduces product amount in "magazine" with given value
        /// </summary>
        /// <param name="amountChange">How many product was sold</param>
        public void ReduceProductstateAmount(Product product, int amountChange)
        {
            try
            {
                var productState = GetProductState(product);
            }
        }

    }
}
