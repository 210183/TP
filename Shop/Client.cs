using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public class Client
    {
        private string id;

        public string Id { get => id; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Client (string firstName, string lastName)
        {
            id = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
