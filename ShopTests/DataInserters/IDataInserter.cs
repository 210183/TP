using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Tests
{
    public interface IDataInserter
    {
        void InitializeContextWithData(ShopContext context);
    }
}
