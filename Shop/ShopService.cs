using Shop.DataHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    class ShopService
    {
        private ShopRepository repository;

        public ShopService(ShopRepository repository)
        {
            this.repository = repository;
        }

        public void SellProduct(Client client, Product product, int amountToSell)
        {
            if (client != null)
            {
                if (product != null)
                {
                    var invoice = new Invoice(client, product);
                    try
                    {
                        //update state
                        var productState = repository.GetProductState(product);
                        if(productState.Amount >= amountToSell)
                        {
                            productState.Amount -= amountToSell;
                            repository.Add(invoice);
                        }
                        else
                        {
                            throw new NotEnoughProductException("Not enough product in stock.");
                        }
                    }
                    catch(NotFoundException e)
                    {

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

    }
}
