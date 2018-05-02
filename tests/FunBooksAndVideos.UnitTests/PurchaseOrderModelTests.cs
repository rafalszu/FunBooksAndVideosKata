using System;
using FunBooksAndVideos.Models;
using Xunit;

namespace FunBooksAndVideos.UnitTests
{
    public class PurchaseOrderModelTests
    {
        [Fact]
        public void ModelHasId()
        {
            PurchaseOrder po = new PurchaseOrder();
            Assert.NotEqual(Guid.Empty, po.Id);
            Assert.NotEqual(string.Empty, po.OrderNumber);
        }

        [Fact]
        public void ModelStoresValuesFromConstructor()
        {
            PurchaseOrder po = new PurchaseOrder(0, new Customer(), new CustomerAddress());
            Assert.NotEqual(Guid.Empty, po.Id);
            Assert.NotEqual(string.Empty, po.OrderNumber);
        }

        [Fact]
        public void ConstructorFailsIfNocustomerOrAddressGiven()
        {
            var exception1 = Record.Exception(() => { PurchaseOrder po = new PurchaseOrder(0, null, null); });
            Assert.NotNull(exception1);
            Assert.IsType<ArgumentNullException>(exception1);

            var exception2 = Record.Exception(() => { PurchaseOrder po = new PurchaseOrder(0, new Customer(), null); });
            Assert.NotNull(exception2);
            Assert.IsType<ArgumentNullException>(exception2);
            
            var exception3 = Record.Exception(() => { PurchaseOrder po = new PurchaseOrder(0, null, new CustomerAddress()); });
            Assert.NotNull(exception3);
            Assert.IsType<ArgumentNullException>(exception3);
        }
    }
}