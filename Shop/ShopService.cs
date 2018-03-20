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
    }
}
