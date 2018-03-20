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
            context.Clients.Add(new Client
            (
                "Buddy",
                "Guy"
            ));
            

        }
    }
}
