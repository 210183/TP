using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public class Invoice
    {
        private string id;

        public string Id { get => id; }
        public DateTime PurchaseTime { get; }
        public Client Client { get; set; }
        public Product Product { get; set; }

        public Invoice(Client client, Product product)
        {
            id = Guid.NewGuid().ToString();
            PurchaseTime = DateTime.Now;
            Product = product;
            Client = client;
        }

        public override string ToString()
        {
            return $"Date {PurchaseTime.ToString()} /nBuyer: {Client.ToString()} \nProduct: {Product.ToString()}";
        }
    }
}
