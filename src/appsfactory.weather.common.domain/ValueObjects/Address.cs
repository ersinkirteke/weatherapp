using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string City { get; private set; }
        public string ZipCode { get; private set; }

        public Address()
        {

        }

        public Address(string city, string zipCode)
        {
            City = city;
            ZipCode = zipCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return City;
            yield return ZipCode;
        }
    }
}
