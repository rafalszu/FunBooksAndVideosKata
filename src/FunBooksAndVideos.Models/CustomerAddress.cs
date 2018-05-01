using System;

namespace FunBooksAndVideos.Models
{
    public class CustomerAddress
    {
        public Guid Id { get; set; }
        public string FriendlyName { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public CustomerAddress() => this.Id = Guid.NewGuid();

        public CustomerAddress(string friendlyName, string streetLine1, string streetLine2, string zipCode, string city, string country) : this()
        {
            this.FriendlyName = friendlyName;
            this.StreetLine1 = streetLine1;
            this.StreetLine2 = streetLine2;
            this.ZipCode = zipCode;
            this.City = city;
            this.Country = country;
        }
    }
}