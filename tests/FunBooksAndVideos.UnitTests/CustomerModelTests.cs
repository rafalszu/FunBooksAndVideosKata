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
    }
}