using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop;
using Shop.Logging;

namespace ConsoleShop
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var logger = new ConsoleLogger();
            var context = new ShopContext();
            var dataInserter = new ConstantDataInserter();
            var repo = new ShopRepository(context, dataInserter, logger);
            var service = new ShopService(repo, logger);
            service.ConsoleShowAll();
            service.ConsoleShowCombined();
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
                    "\n--- 3 - ");
                chosenOption = ReadKey();
                switch(chosenOption)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Choose number of client you want to become");
                        chosenClientNumber = ReadKey();
                        if (chosenClientNumber <= service.GetAllClients().Count && chosenClientNumber > 0)
                        {
                            currentClient = service.GetAllClients().ToList()[chosenClientNumber-1];
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Choose number of product you want to buy");
                        chosenProductNumber = ReadKey();
                        if (chosenProductNumber <= service.GetAllProducts().Count && chosenProductNumber > 0)
                        {
                            chosenProduct = service.GetAllProducts().ToList()[chosenProductNumber-1];
                            service.SellProduct(currentClient, chosenProduct, 1);
                        }
                        break;
                    default:
                        Console.WriteLine("Unknown option");
                        break;
                }
            }
            
            Console.ReadLine();

            int ReadKey()
            {
               var chosen = Console.ReadKey();
               char c = chosen.KeyChar;
               return (int)c - 48;
            }
        }
    }
}
