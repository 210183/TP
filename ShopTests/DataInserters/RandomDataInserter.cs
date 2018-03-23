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
        private static readonly int clientAmount = 1000;
        private static readonly int productAmount = 100;
        public void InitializeContextWithData(ShopContext context)
        {
            var clients = new Client[clientAmount];
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
            #region generate products
            var products = new Product[productAmount];
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
            Random Randomizer = new Random();
            for (int i = 0; i < productAmount; i++)
            {
                products[i] = new Product(
                    productNames[Randomizer.Next() % productNames.Length] +
                    productNames[Randomizer.Next() % productNames.Length]
                    );
                context.Products.Add(products[i].Id, products[i]);
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
