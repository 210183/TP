using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop;
using Shop.DataHandling;
using Shop.Logging;

namespace ConsoleShop
{
    class Program
    {
        private static readonly string SuperSecretPassword = "haslomaslo";

        static void Main(string[] args)
        {
            var logger = new ConsoleLogger();
            var context = new ShopContext();
            var repo = new ShopRepository(context, logger);
            var service = new ShopService(repo, logger);

            #region loop vars
            var shouldMainLoop = true;
            int chosenOption = 1;
            var currentClient = new Client("Steve", "Harris");
            var chosenProduct = new Product("Squire");
            int chosenClientNumber = 1;
            int chosenProductNumber = 1;
            #endregion
            while (shouldMainLoop)
            {
                Console.Clear();
                service.ConsoleShowAll();
                Console.WriteLine("\n\n\nWrite appropriate number for action:" +
                    "\n--- 1 - Choose which client you are" +
                    "\n--- 2 - Buy item" +
                    "\n--- 3 - Add new item [Admin-Only]" +
                    "\n--- 4 - Add new Client [Admin-Only]" +
                    "\n--- 5 - Delete Client [Admin-Only]" +
                    "\n--- 6 - Delete Product [Admin-Only]" +
                    "\n--- 7 - Show Combined Report" +
                    "\n--- 8 - End");
                chosenOption = ReadKey();
                switch(chosenOption)
                {
                    case 1:
                        Console.Clear();
                        ChangeClient();
                        break;
                    case 2:
                        Console.Clear();
                        BuyProduct();
                        break;
                    case 3:
                        Console.Clear();
                        AddProduct();
                        break;
                    case 4:
                        Console.Clear();
                        AddClient();
                        break;
                    case 5:
                        Console.Clear();
                        DeleteClient();
                        break;
                    case 6:
                        Console.Clear();
                        DeleteProduct();
                        break;
                    case 7:
                        Console.Clear();
                        service.ConsoleShowCombined();
                        Console.Read();
                        break;
                    case 8:
                        shouldMainLoop = false;
                        break;
                    default:
                        Console.WriteLine("Unknown option");
                        Console.Read();
                        break;
                }
            }
            
            Console.ReadLine();

            #region local methods
            int ReadKey()
            {
               var chosen = Console.ReadKey();
               char c = chosen.KeyChar;
               return (int)c - 48;
            }
            bool CheckPassword()
            {
                Console.WriteLine("Password please: ");
                if (Console.ReadLine() == SuperSecretPassword)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("You shall not pass!!!");
                    Console.Read();
                    return false;
                }
            }

            void ChangeClient()
            {
                Console.WriteLine("Choose number of client you want to become");
                chosenClientNumber = ReadKey();
                if (chosenClientNumber <= service.GetAllClients().Count && chosenClientNumber > 0)
                {
                    currentClient = service.GetAllClients().ToList()[chosenClientNumber - 1];
                }
            }
            void BuyProduct()
            {
                Console.WriteLine("Choose number of product you want to buy");
                chosenProductNumber = ReadKey();
                if (chosenProductNumber <= service.GetAllProducts().Count && chosenProductNumber > 0)
                {
                    chosenProduct = service.GetAllProducts().ToList()[chosenProductNumber - 1];
                    try
                    {
                        service.SellProduct(currentClient, chosenProduct, 1);
                    }
                    catch (NotEnoughProductException)
                    {
                        Console.WriteLine($"\nStock is out of {chosenProduct.ToString()}");
                        Console.Read();
                    }
                }
            }
            void AddProduct()
            {
                if (CheckPassword())
                {
                    Console.WriteLine("What is new product name?");
                    var productName = Console.ReadLine();
                    Console.WriteLine("How much of that do we have in stock?");
                    var amountAsString = Console.ReadLine();
                    int amount;
                    if (Int32.TryParse(amountAsString, out amount))
                    {
                        if (amount >= 0)
                        {
                            Console.WriteLine("What is the price?");
                            var priceAsString = Console.ReadLine();
                            decimal price;
                            if (Decimal.TryParse(priceAsString, out price))
                            {
                                if (price >= 0)
                                {
                                    var newProduct = new Product(productName);
                                    var newProductState = new ProductState(newProduct, amount, price, new Percentage(0.23));
                                    context.Products.Add(newProduct.Id, newProduct);
                                    context.ProductStates.Add(newProductState);
                                }
                                else
                                {
                                    Console.WriteLine("Price must be a positive value");
                                    Console.Read();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Could not read that price");
                                Console.Read();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Amount must be a positive value");
                            Console.Read();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Could not read that amount.");
                        Console.Read();
                    }
                }
            }
            void AddClient()
            {
                if (CheckPassword())
                {
                    Console.WriteLine("What is new client first name?");
                    var clientFirstName = Console.ReadLine();
                    Console.WriteLine("What is new client second name?");
                    var clientSecondName = Console.ReadLine();
                    context.Clients.Add(new Client(clientFirstName, clientSecondName));
                }
            }
            void DeleteClient()
            {
                if (CheckPassword())
                {
                    Console.WriteLine("Choose client number?");
                    int num = ReadKey();
                    if (num <= service.GetAllClients().Count)
                    {
                        var client = service.GetAllClients().ToList()[num - 1];
                        service.Delete(client);
                    }
                }
            }
            void DeleteProduct()
            {
               if(CheckPassword())
                {

                    Console.WriteLine("Choose product number?");
                    int num = ReadKey();
                    if (num <= service.GetAllProducts().Count)
                    {
                        var product = service.GetAllProducts().ToList()[num - 1];
                        service.Delete(product);
                    }
                }
            }
            #endregion
        }
    }
}
