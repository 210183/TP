using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model
{
    public class ClientData
    {
        public string FirstName { get => FirstName; set { FirstName = value; IsFirstNameChanged = true; } }
        public bool IsFirstNameChanged { get; set; } = false;
        public string LastName { get => LastName; set { LastName = value; IsLastNameChanged = true; } }
        public bool IsLastNameChanged { get; set; } = false;
    }
}
