using System;
using FunBooksAndVideos.Models;
using FunBooksAndVideos.Models.Exceptions;
using Xunit;

namespace FunBooksAndVideos.UnitTests
{
    public class CustomerModelTests
    {
        [Fact]
        public void ModelHasId()
        {
            Customer c1 = new Customer();
            Assert.NotEqual(Guid.Empty, c1.Id);

            Customer c2 = new Customer("John", "Doe");
            Assert.NotEqual(Guid.Empty, c2.Id);
        }

        [Fact]
        public void ModelHasFirstNameLastName()
        {
            Customer c2 = new Customer("John", "Doe");
            Assert.NotEqual(string.Empty, c2.FirstName);
            Assert.NotEqual(string.Empty, c2.LastName);
        }

        [Fact]
        public void ConstructorFailsIfArgumentsAreEmpty()
        {
            var exception1 = Record.Exception(() => {
                Customer c = new Customer("", "");
            });
            Assert.NotNull(exception1);
            Assert.IsType<ArgumentNullException>(exception1);

            var exception2 = Record.Exception(() => {
                Customer c = new Customer("first name", "");
            });
            Assert.NotNull(exception2);
            Assert.IsType<ArgumentNullException>(exception2);

            var exception3 = Record.Exception(() => {
                Customer c = new Customer("", "last name");
            });
            Assert.NotNull(exception3);
            Assert.IsType<ArgumentNullException>(exception3);
        }

        [Fact]
        public void CanAddAddresstoCustomer()
        {
            var ex = Record.Exception(() => {
                Customer c = new Customer("first", "last");
                c.Addresses.Add(new CustomerAddress());
            });
            Assert.Null(ex);
        }

        [Fact]
        public void CustomerValidationFailsIfNoAddressGiven()
        {
            var ex = Record.Exception(() => {
                Customer c = new Customer("first name", "last name");
                c.Validate();
            });
            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
        }

        [Fact]
        public void CustomerValidationFailsIfAnyOfGivenAddressesIsNotValid()
        {
            var ex = Record.Exception(() => {
                Customer c = new Customer("first", "last");
                c.Addresses.Add(new CustomerAddress());

                c.Validate();
            });
            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
        }

        [Fact]
        public void CustomerIsValidWhenAllDataFilledOut()
        {
            var ex = Record.Exception(() => {
                Customer c = new Customer("first", "last");
                c.Addresses.Add(new CustomerAddress("a name", "s1", null, "zipcode", "city", "country"));

                c.Validate();
            });
            Assert.Null(ex);
        }
    }
}