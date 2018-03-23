using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model
{
    public class ProductData
    {
        public string Name { get => Name; set { Name = value; IsNameChanged = true; } }
        public bool IsNameChanged { get; set; } = false;
    }
}
