using System;
using FunBooksAndVideos.Models;
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
    }
}