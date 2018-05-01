using System;
using FunBooksAndVideos.Models;
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
    }
}