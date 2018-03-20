using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    class ShopContext
    {
        public List<Client> Clients { get; set; }
        public Dictionary<string, Product> Products { get; set; }
        public ObservableCollection<Invoice> Invoices { get; set; }
        public ObservableCollection<ProductState> ProductStates { get; set; }
    }
}
