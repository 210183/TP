using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Tests
{
    public class RandomDataInserter : IDataInserter
    {
        private const int _defaultClientAmount = 1000;
        private const int _defaultProductAmount = 100;
        private const int _defaultInvoicesAmount = 2000;

        private static readonly int maxAmount = 200;
        private static readonly int maxPrice = 20000;
        private static readonly int maxPercentage = 100;

        private int clientAmount;
        private int productAmount;
        private int invoicesAmount;

        public RandomDataInserter(int clientAmount = _defaultClientAmount, int productAmount = _defaultProductAmount, int invoicesAmount = _defaultInvoicesAmount)
        {
            this.clientAmount = clientAmount;
            this.productAmount = productAmount;
            this.invoicesAmount = invoicesAmount;
        }

        /// <summary>
        /// Inserts randomly generated, by default or specified, 1000 clients, 100 product(max: amount = 200, price =20000)
        /// and creates 2000 invoices between them
        /// </summary>
        /// <param name="context"></param>
        public void InitializeContextWithData(ShopContext context)
        {
            var clients = new Client[clientAmount];
            var products = new Product[productAmount];
            var productStates = new ProductState[productAmount];
            var invoices = new Invoice[invoicesAmount];
            var assembly = Assembly.GetExecutingAssembly();
            string[] stringSeparators = new string[] { "\r\n" };
            #region generate clients
            { //scope to throw away splitted names when not necessary  
                var firstNameSourceName = FormatResourceName(assembly, "Resources/first-names.txt");
                var lastNameSourceName = FormatResourceName(assembly, "Resources/last-names.txt");
                string firstNamesSequence;
                string lastNamesSequence;
                using (Stream stream = assembly.GetManifestResourceStream(firstNameSourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        firstNamesSequence = reader.ReadToEnd();
                    }
                }
                using (Stream stream = assembly.GetManifestResourceStream(lastNameSourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        lastNamesSequence = reader.ReadToEnd();
                    }
                }
                string[] firstNames = firstNamesSequence.Split(stringSeparators, StringSplitOptions.None);
                string[] lastNames = lastNamesSequence.Split(stringSeparators, StringSplitOptions.None);
                Random firstRandomizer = new Random();
                Random lastRandomizer = new Random();
                for (int i = 0; i < clientAmount; i++)
                {
                    clients[i] = new Client(
                        firstNames[firstRandomizer.Next() % firstNames.Length],
                        lastNames[lastRandomizer.Next() % lastNames.Length]
                        );
                    context.Clients.Add(clients[i]);
                }
            }
            #endregion
            #region generate products and their states
            {
                var productsSourceName = FormatResourceName(assembly, "Resources/products.txt");
                string productsSequence;
                using (Stream stream = assembly.GetManifestResourceStream(productsSourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        productsSequence = reader.ReadToEnd();
                    }
                }
                string[] productNames = productsSequence.Split(stringSeparators, StringSplitOptions.None);
                Random randomizer = new Random();
                for (int i = 0; i < productAmount; i++)
                {
                    products[i] = new Product(
                        productNames[randomizer.Next() % productNames.Length] +
                        productNames[randomizer.Next() % productNames.Length]
                        );
                    context.Products.Add(products[i].Id, products[i]);
                    productStates[i] = new ProductState(
                        products[i],
                        randomizer.Next() % maxAmount,
                        randomizer.Next() % maxPrice,
                        new Percentage(randomizer.Next() & maxPercentage)
                        );
                    context.ProductStates.Add(productStates[i]);
                }
            }
            #endregion
            #region generate invoices
            {
                var randomizer = new Random();
                for (int i = 0; i < invoicesAmount; i++)
                {
                    var currentBuyer = clients[randomizer.Next() % clients.Length];
                    var currentProduct = products[randomizer.Next() % products.Length];
                    invoices[i] = new Invoice(
                        currentBuyer,
                        currentProduct,
                        randomizer.Next() % 10,
                        randomizer.Next() % maxPrice,
                        new Percentage(randomizer.Next() & maxPercentage)
                        );
                    context.Invoices.Add(invoices[i]);
                }
            }
            #endregion
            }
        private static string FormatResourceName(Assembly assembly, string resourceName)
        {
            return assembly.GetName().Name + "." + resourceName.Replace(" ", "_")
                                                               .Replace("\\", ".")
                                                               .Replace("/", ".");
        }
    }
}
