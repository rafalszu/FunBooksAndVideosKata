using System;
using FunBooksAndVideos.Models.Exceptions;
using FunBooksAndVideos.Models.Interfaces;

namespace FunBooksAndVideos.Models
{
    public class CustomerAddress : ModelBase, IValidation
    {
        public string FriendlyName { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public CustomerAddress() : base()
        {}

        public CustomerAddress(string friendlyName, string streetLine1, string streetLine2, string zipCode, string city, string country) : this()
        {
            if(string.IsNullOrWhiteSpace(friendlyName))
                throw new ArgumentNullException(nameof(friendlyName));
            if(string.IsNullOrWhiteSpace(streetLine1) && string.IsNullOrWhiteSpace(streetLine2))
                throw new ValidationErrorException("streetline1 or streetline2 need to have a value");

            if(string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentNullException(nameof(zipCode));
            if(string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException(nameof(city));
            if(string.IsNullOrWhiteSpace(country))
                throw new ArgumentNullException(nameof(country));

            this.FriendlyName = friendlyName;
            this.StreetLine1 = streetLine1;
            this.StreetLine2 = streetLine2;
            this.ZipCode = zipCode;
            this.City = city;
            this.Country = country;
        }

        public void Validate()
        {
            if(string.IsNullOrWhiteSpace(FriendlyName))
                throw new ValidationErrorException(nameof(FriendlyName));
            if(string.IsNullOrWhiteSpace(StreetLine1) && string.IsNullOrWhiteSpace(StreetLine2))
                throw new ValidationErrorException("streetline1 or streetline2 need to have a value");
            if(string.IsNullOrWhiteSpace(ZipCode))
                throw new ValidationErrorException(nameof(ZipCode));
            if(string.IsNullOrWhiteSpace(City))
                throw new ValidationErrorException(nameof(City));
            if(string.IsNullOrWhiteSpace(Country))
                throw new ValidationErrorException(nameof(Country));
        }
    }
}