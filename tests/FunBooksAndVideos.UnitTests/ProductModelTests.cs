using System;
using FunBooksAndVideos.Models;
using FunBooksAndVideos.Models.Exceptions;
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

        [Fact]
        public void ConstructorFailsIfProductTypeIsNull()
        {
            var exception = Record.Exception(() => { Product p = new Product("somenthing", null); });
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void ProductIsNotValidWhenNameIsMissing()
        {
            var ex = Record.Exception(() => {
                Product p = new Product();
                p.Type = new BookProductType();

                p.Validate();
            });
            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
        }

        [Fact]
        public void ProductIsNotValidWhenProductTypeIsMissing()
        {
            var ex = Record.Exception(() => {
                Product p = new Product();
                p.Name = "something";

                p.Validate();
            });
            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
        }

        [Fact]
        public void ProductIsValidWhenAllDataIsFilledIn()
        {
            var ex = Record.Exception(() => {
                Product p = new Product("pro", new BookProductType());

                p.Validate();
            });
            Assert.Null(ex);
        }
    }
}