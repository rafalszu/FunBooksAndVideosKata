using System;
using System.Collections.Generic;

namespace FunBooksAndVideos.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<CustomerAddress> Addresses { get; set; }
        public CustomerMembershipType MembershipType { get; set; }

        public Customer() => this.Id = Guid.NewGuid();

        public Customer(string firstName, string lastName) : this()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}