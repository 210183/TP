using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    /// <summary>
    /// Represents data context for application.
    /// Consists of collections: clients, products, products states, invoices.
    /// Also holds report data for last generated context report.
    /// </summary>
    public class ShopContext
    {
        public List<Client> Clients { get; set; } = new List<Client>();
        public Dictionary<string, Product> Products { get; set; } = new Dictionary<string, Product>();
        public ObservableCollection<Invoice> Invoices { get; set; } = new ObservableCollection<Invoice>();
        public ObservableCollection<ProductState> ProductStates { get; set; } = new ObservableCollection<ProductState>();

        public ReportData ReportData { get; set; } = new ReportData();
    }
}
