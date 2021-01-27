using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Store.PartnerCenter.Models;

namespace Library
{
    class Publisher
    {
        private string _name;
        public Address Address { get;}
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentException("Invalid argument");
                _name = value;
            }
        }
        private Publisher(string name, Address address)
        {
            Name = name;
            Address = address;
        }
        public static Publisher create(string name, string city, string region, string country, string postalCode)
        {
            Address address = new Address();
            address.City = city;
            address.Region = region;
            address.Country = country;
            address.PostalCode = postalCode;
            return new Publisher(name, address);
        }
        public override string ToString()
        {
            return $"{Name}\n Address:{Address.City}, {Address.Region}, {Address.Country}, {Address.PostalCode}";
        }
    }
}
