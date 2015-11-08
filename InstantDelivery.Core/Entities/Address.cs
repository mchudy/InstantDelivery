using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstantDelivery.Core.Entities
{
    [ComplexType]
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}