using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonConfigForMyEntities.Model
{
    public class Person
    {
        private IList<Address> _addresses = new List<Address>();

        private Person() { }    // Making JSON.NET happy.

        public Person(string fullName)
        {
            FullName = fullName;
        }

        public Person(string fullName, IEnumerable<Address> addresses)
        {
            FullName = fullName;
            _addresses = addresses.ToList();
        }

        public string FullName { get; private set; }

        public IEnumerable<Address> Addresses => _addresses;

        public void AddAddress(string address)
        {
            _addresses.Add(new Address { Name = address });
        }
    }
}
