using System;
using FunBooksAndVideos.Models;
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
    }
}