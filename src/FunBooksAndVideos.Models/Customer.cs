using System;
using System.Collections.Generic;
using System.Linq;
using FunBooksAndVideos.Models.Exceptions;
using FunBooksAndVideos.Models.Interfaces;

namespace FunBooksAndVideos.Models
{
    public class Customer : ModelBase, IValidation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
        public CustomerMembershipType MembershipType { get; set; }

        public Customer() : base()
        {
            this.Addresses = new List<CustomerAddress>();
            this.MembershipType = CustomerMembershipType.None;
        }

        public Customer(string firstName, string lastName) : this()
        {
            if(string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));
            if(string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));
            
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public void Validate()
        {
            if(string.IsNullOrWhiteSpace(this.FirstName))
                throw new ValidationErrorException(nameof(FirstName));
            if(string.IsNullOrWhiteSpace(this.LastName))
                throw new ValidationErrorException(nameof(LastName));
            if(this.Addresses == null || !this.Addresses.Any())
                throw new ValidationErrorException(nameof(Addresses));

            this.Addresses.ForEach(x => x.Validate());
        }
    }
}