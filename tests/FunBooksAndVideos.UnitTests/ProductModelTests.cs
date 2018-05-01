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
    }
}