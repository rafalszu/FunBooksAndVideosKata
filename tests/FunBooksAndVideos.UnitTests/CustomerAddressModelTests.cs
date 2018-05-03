using System;
using FunBooksAndVideos.Models;
using FunBooksAndVideos.Models.Exceptions;
using Xunit;

namespace FunBooksAndVideos.UnitTests
{
    public class CustomerAddressModelTests
    {
        [Fact]
        public void ModelHasId()
        {
            CustomerAddress addr1 = new CustomerAddress();
            Assert.NotEqual(Guid.Empty, addr1.Id);

            CustomerAddress addr2 = new CustomerAddress("my address", "street 1", "street 2", "zipcode", "city", "country");
            Assert.NotEqual(Guid.Empty, addr2.Id);
        }

        [Fact]
        public void ModelStoresValuesFromConstructor()
        {
            CustomerAddress address = new CustomerAddress("my address", "street 1", "street 2", "zipcode", "city", "country");
            Assert.Equal("my address", address.FriendlyName);
            Assert.Equal("street 1", address.StreetLine1);
            Assert.Equal("street 2", address.StreetLine2);
            Assert.Equal("zipcode", address.ZipCode);
            Assert.Equal("city", address.City);
            Assert.Equal("country", address.Country);
        }

        [Fact]
        public void ConstructorFailsIfUncompleteDataPassed()
        {
            var exception1 = Record.Exception(() => {
                CustomerAddress c = new CustomerAddress("", "", "", "", "", "");
            });
            Assert.NotNull(exception1);
            Assert.IsType<ArgumentNullException>(exception1);
            
            var exception2 = Record.Exception(() => {
                CustomerAddress c = new CustomerAddress("name", null, null, null, null, null);
            });
            Assert.NotNull(exception2);
            Assert.IsType<ValidationErrorException>(exception2);

            var exception3 = Record.Exception(() => {
                CustomerAddress c = new CustomerAddress("name", "street1", null, "", "", "");
            });
            Assert.NotNull(exception3);
            Assert.IsType<ArgumentNullException>(exception3);

            var exception4 = Record.Exception(() => {
                CustomerAddress c = new CustomerAddress("name", "street1", "", "zip", "", "country");
            });
            Assert.NotNull(exception4);
            Assert.IsType<ArgumentNullException>(exception4);

            var exception5 = Record.Exception(() => {
                CustomerAddress c = new CustomerAddress("name", "street1", "", "zip", "city", "");
            });
            Assert.NotNull(exception5);
            Assert.IsType<ArgumentNullException>(exception5);
        }

        [Fact]
        public void CustomerAddressIsNotValidIfAnyOfRequiredFieldsIsMissing()
        {
            var ex1 = Record.Exception(() => {
                CustomerAddress ca = new CustomerAddress();
                ca.FriendlyName = "name";

                ca.Validate();
            });
        }

        [Fact]
        public void CustomerAddressIsValidWhenAllDataIsFilledIn()
        {
            var ex = Record.Exception(() => {
                CustomerAddress addr = new CustomerAddress
                {
                    City = "city",
                    Country = "country",
                    ZipCode = "zipcode",
                    FriendlyName = "friendly name",
                    StreetLine1 = "street 1",
                };

                addr.Validate();
            });

            Assert.Null(ex);
        }
    }
}