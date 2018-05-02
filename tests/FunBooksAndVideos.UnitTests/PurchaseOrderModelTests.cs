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
            Assert.Throws<ArgumentNullException>(() => { PurchaseOrder po = new PurchaseOrder(0, null, null); });
            Assert.Throws<ArgumentNullException>(() => { PurchaseOrder po = new PurchaseOrder(0, new Customer(), null); });
            Assert.Throws<ArgumentNullException>(() => { PurchaseOrder po = new PurchaseOrder(0, null, new CustomerAddress()); });
        }
    }
}