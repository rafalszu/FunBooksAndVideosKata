using System;

namespace FunBooksAndVideos.Models
{
    public class CustomerAddress : ModelBase
    {
        public string FriendlyName { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public CustomerAddress() : base()
        {}

        public CustomerAddress(string friendlyName, string streetLine1, string streetLine2, string zipCode, string city, string country) : base()
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