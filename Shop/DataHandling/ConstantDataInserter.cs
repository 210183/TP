using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    class ConstantDataInserter : IDataInserter
    {
        public void InitializeContextWithData(ShopContext context)
        {
            var names = new List<string>()
            {
                "Buddy",
                "Peter",
                "Tonny",
                "Janick",
                "Johnny"
            };
            var lastNames = new List<string>()
            {
                "Guy",
                "Green",
                "Iommi",
                "Gers",
                "Cash"
            };
            var products = new Product[4];
            products[0] = new Product("Fender Stratocaster");
            products[1] = new Product("Fender Telecaster");
            products[2] = new Product("Jaydee SG");
            products[3] = new Product("Gibson Les Paul Standard");
            for (int i=0; i <names.Capacity; i++)
            {
                context.Clients.Add(new Client(
                    names[i],
                    lastNames[i]
                    ));
            }
            
            //var clientToAdd = new Client(
            //    "Buddy",
            //    "Guy");
            //context.Clients.Add(clientToAdd);
            //clientToAdd = new Client(
            //    "Peter",
            //    "Green");
            //context.Clients.Add(clientToAdd);
            //clientToAdd = new Client(
            //    "Tonny",
            //    "Iommi");
            //context.Clients.Add(clientToAdd);
            //clientToAdd = new Client(
            //    "Janick",
            //    "Gers");
            //context.Clients.Add(clientToAdd);
            //clientToAdd = new Client(
            //    "Johnny",
            //    "Cash");
            //context.Clients.Add(clientToAdd);

        }
    }
}
