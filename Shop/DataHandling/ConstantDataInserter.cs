using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public class ConstantDataInserter : IDataInserter
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
            var states = new ProductState[4];
            products[0] = new Product("Fender Stratocaster");
            states[0] = new ProductState(products[0], 5, (decimal)2200, new Percentage(0.23));
            products[1] = new Product("Fender Telecaster");
            states[1] = new ProductState(products[1], 2, (decimal)2101.12, new Percentage(0.23));
            products[2] = new Product("Jaydee SG");
            states[2] = new ProductState(products[2], 1, (decimal)21321.42, new Percentage(0.23));
            products[3] = new Product("Gibson Les Paul Standard");
            states[3] = new ProductState(products[3], 3, (decimal)11231.55, new Percentage(0.23));

            for (int i=0; i <names.Count; i++)
            {
                context.Clients.Add(new Client(
                    names[i],
                    lastNames[i]
                    ));
            }
            for (int i = 0; i < products.Length; i++)
            {
                context.Products.Add(products[i].Id, products[i]);
                context.ProductStates.Add(states[i]);
            }
            var clients = context.Clients;
            context.Invoices.Add(new Invoice(
                clients[0],
                products[1]
                ));
            context.Invoices.Add(new Invoice(
                clients[0],
                products[2]
                ));
            context.Invoices.Add(new Invoice(
                clients[1],
                products[3]
                ));
            context.Invoices.Add(new Invoice(
                clients[3],
                products[3]
                ));
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
