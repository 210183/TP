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
            Console.ReadLine();

        }
    }
}
