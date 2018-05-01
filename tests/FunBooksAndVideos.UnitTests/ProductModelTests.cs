using System;
using FunBooksAndVideos.Models;
using Xunit;

namespace FunBooksAndVideos.UnitTests
{
    public class ProductModelTests
    {
        [Fact]
        public void ModelHasId()
        {
            Product p = new Product();
            Assert.NotEqual(Guid.Empty, p.Id);
        }

        [Fact]
        public void ModelStoresValuesFromConstructor()
        {
            Product p = new Product("something", new BookProductType());
            Assert.Equal("something", p.Name);
            Assert.IsType<BookProductType>(p.Type);
        }
    }
}