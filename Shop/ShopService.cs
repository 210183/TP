using Shop.DataHandling;
using Shop.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public class ShopService
    {
        private ShopRepository repository;
        private ILogger logger;
        public ShopService(ShopRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        #region GetAll
        public ICollection<Client> GetAllClients()
        {
            return repository.GetAllClients();
        }
        public ICollection<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }
        public ICollection<ProductState> GetAllProductStates()
        {
            return repository.GetAllProductStates();
        }
        public ICollection<Invoice> GetAllInvoices()
        {
            return repository.GetAllInvoices();
        }
        #endregion

        public void SellProduct(Client client, Product product, int amountToSell)
        {
            if (client != null)
            {
                if (product != null)
                {
                    try
                    {
                        //update state
                        var productState = repository.GetProductState(product);
                        if(productState.Amount >= amountToSell)
                        {
                            productState.Amount -= amountToSell;
                            var invoice = new Invoice(client, product);
                            repository.Add(invoice);
                            logger.Log($"New purchase has been made with invoice: {invoice.ToString()}");
                        }
                        else
                        {
                            throw new NotEnoughProductException("Not enough product in stock.");
                        }
                    }
                    catch(NotFoundException)
                    {
                        logger.Log("Couldn't find product state for given product. Stopped selling procedure.", LogLevel.Critical);
                    }
                }
                else
                {
                    throw new ArgumentNullException("You cannot sell unknown product");
                }
            }
            else
            {
                throw new ArgumentNullException("You cannot sell to nobody");
            }
        }

        public void ConsoleShowAll()
        {
            var color = ConsoleColor.Cyan;
            void WriteLine(string msg) {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(msg);
                Console.ForegroundColor = oldColor;
            };

            WriteLine("\n_C_L_I_E_N_T_S");
            color = ConsoleColor.DarkCyan;
            foreach(var item in repository.GetAllClients())
            {
                WriteLine(item.ToString() + "\n");
            }
            color = ConsoleColor.Yellow;
            WriteLine("\n_P_R_O_D_U_C_T_S");
            color = ConsoleColor.DarkYellow;
            foreach (var item in repository.GetAllProducts())
            {
                WriteLine(item.ToString() + "\n");
            }
            color = ConsoleColor.Green;
            WriteLine("\n_P_R_O_D_U_C_T_S_-_S_T_A_T_E_S_");
            color = ConsoleColor.DarkGreen;
            foreach (var item in repository.GetAllProductStates())
            {
                WriteLine(item.ToString() + "\n");
            }
            color = ConsoleColor.Magenta;
            WriteLine("\n_I_N_V_O_I_C_E_S");
            color = ConsoleColor.DarkMagenta;
            foreach (var item in repository.GetAllInvoices())
            {
                WriteLine(item.ToString() + "\n");
            }
        }
        public void ConsoleShowCombined()
        {
            StringBuilder messageBuilder = new StringBuilder();
            foreach(var client in repository.GetAllClients())
            {
                messageBuilder.Append($"\nClient: {client.ToString()}");
                var invoices = repository.GetAllInvoices().Where(i => i.Client.Id == client.Id);
                int ordinalNumber = 0;
                foreach (var invoice in invoices)
                {
                    ordinalNumber++;
                    messageBuilder.Append($"\n--Invoice {ordinalNumber}: {invoice.ToString()}");
                    messageBuilder.Append($"\n-----Product: {invoice.Product.ToString()}");
                    try
                    {
                        var productState = repository.GetProductState(invoice.Product);
                        messageBuilder.Append($"\n-----State: {productState.ToString()}");
                    }
                    catch(NotFoundException)
                    {
                        messageBuilder.Append($"\n-----State is unknown");
                    }
                }
            }
            #region create report from builder
            var report = messageBuilder.ToString();
            var reportData = repository.GetReportData();
            if (reportData.IsReportOutdated())
            {
                reportData.UpdateReportDate();
            }
            reportData.LastCombinedReport = report;
            #endregion
            Console.WriteLine(report);
        }
    }
}
