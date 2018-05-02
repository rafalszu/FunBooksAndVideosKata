using System;
using System.Collections.Generic;

namespace FunBooksAndVideos.Models
{
    public class Customer : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
        public CustomerMembershipType MembershipType { get; set; }

        public Customer() : base()
        {}

        public Customer(string firstName, string lastName) : base()
        {
            if(string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));
            if(string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));
            
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}