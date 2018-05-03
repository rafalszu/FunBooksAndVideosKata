using System;
using FunBooksAndVideos.Models;
using FunBooksAndVideos.Models.Exceptions;
using Xunit;

namespace FunBooksAndVideos.UnitTests
{
    public class PurchaseOrderLineTests
    {
        [Fact]
        public void ModelHasId()
        {
            PurchaseOrderLine line = new PurchaseOrderLine();
            Assert.NotEqual(Guid.Empty, line.Id);
        }

        [Fact]
        public void ModelStoresValuesFromConstructor()
        {
            PurchaseOrderLine line = new PurchaseOrderLine(new Product());
            Assert.NotNull(line.Product);
        }

        [Fact]
        public void ConstructorFailsIfProductArgumentIsNull()
        {
            var exception = Record.Exception(() => { PurchaseOrderLine line = new PurchaseOrderLine(null); });
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void OrderLineNotValidWhenProductIsNull()
        {
            var ex = Record.Exception(() => {
                PurchaseOrderLine line = new PurchaseOrderLine();
                line.Validate();
            });
            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
        }

        [Fact]
        public void OrderLineNotValidWhenProductNotValid()
        {
            var ex = Record.Exception(() => {
                PurchaseOrderLine line = new PurchaseOrderLine();
                line.Product = new Product();

                line.Validate();
            });

            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
        }

        [Fact]
        public void OrderLineValidWhenAllDataFilledOut()
        {
            var ex = Record.Exception(() => {
                PurchaseOrderLine line = new PurchaseOrderLine();
                line.Product = new Product("somethin", new BookProductType());
            });

            Assert.Null(ex);
        }
    }
}