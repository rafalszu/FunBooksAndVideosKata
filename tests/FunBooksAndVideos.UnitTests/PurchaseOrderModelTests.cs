using System;
using FunBooksAndVideos.Models;
using FunBooksAndVideos.Models.Exceptions;
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
        }

        [Fact]
        public void ModelStoresValuesFromConstructor()
        {
            PurchaseOrder po = new PurchaseOrder(1, new Customer(), new CustomerAddress());
            Assert.NotEqual(Guid.Empty, po.Id);
        }

        [Fact]
        public void OrderHasNumberFilledOut()
        {
            PurchaseOrder po1 = new PurchaseOrder();
            Assert.NotEqual(string.Empty, po1.OrderNumber);

            PurchaseOrder po2 = new PurchaseOrder(1, new Customer(), new CustomerAddress());
            Assert.NotEqual(string.Empty, po2.OrderNumber);
        }

        [Fact]
        public void CantRegisterNegativePurchaseOrders()
        {
            var ex = Record.Exception(() => {
                PurchaseOrder po = new PurchaseOrder(-1, new Customer(), new CustomerAddress());
            });
            Assert.NotNull(ex);
            Assert.IsType<ArgumentOutOfRangeException>(ex);
        }

        [Fact]
        public void ConstructorFailsIfNocustomerOrAddressGiven()
        {
            var exception1 = Record.Exception(() => { PurchaseOrder po = new PurchaseOrder(1, null, null); });
            Assert.NotNull(exception1);
            Assert.IsType<ArgumentNullException>(exception1);

            var exception2 = Record.Exception(() => { PurchaseOrder po = new PurchaseOrder(1, new Customer(), null); });
            Assert.NotNull(exception2);
            Assert.IsType<ArgumentNullException>(exception2);
            
            var exception3 = Record.Exception(() => { PurchaseOrder po = new PurchaseOrder(1, null, new CustomerAddress()); });
            Assert.NotNull(exception3);
            Assert.IsType<ArgumentNullException>(exception3);
        }

        [Fact]
        public void CanAddOrderLine()
        {
            var ex = Record.Exception(() => {
                PurchaseOrder po = new PurchaseOrder();
                po.OrderLines.Add(new PurchaseOrderLine());
            });

            Assert.Null(ex);
        }

        [Fact]
        public void OrderNotValidWhenCustomerIsMissing()
        {
            var ex = Record.Exception(() => {
                PurchaseOrder po = new PurchaseOrder();
                po.TotalValue = 1;
                po.ShippingAddress = new CustomerAddress();
                po.OrderLines.Add(new PurchaseOrderLine());

                po.Validate();
            });
            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
            Assert.Equal("Customer", ex.Message);
        }

        [Fact]
        public void OrderNotValidWhenCustomerNotValid()
        {
            var ex = Record.Exception(() => {
                CustomerAddress addr = new CustomerAddress("name", "street1", null, "zip", "city", "country");
                PurchaseOrder po = new PurchaseOrder();
                po.TotalValue = 1;
                po.ShippingAddress = addr;
                po.Customer = new Customer();

                po.Validate();
            });
            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
            Assert.Equal("FirstName", ex.Message);
        }

        [Fact]
        public void OrderNotValidWhenShippingAddressIsMissing()
        {
            var ex = Record.Exception(() => {
                Customer customer = new Customer("first", "last");
                customer.Addresses.Add(new CustomerAddress("name", "street1", null, "zip", "city", "country"));
                
                PurchaseOrder po = new PurchaseOrder();
                po.TotalValue = 1;
                po.Customer = customer;

                po.Validate();
            });
            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
            Assert.Equal("ShippingAddress", ex.Message);
        }

        [Fact]
        public void OrderNotValidWhenAddressNotValid()
        {
            var ex = Record.Exception(() => {
                Customer customer = new Customer("first", "last");
                customer.Addresses.Add(new CustomerAddress("name", "street1", null, "zip", "city", "country"));

                PurchaseOrder po = new PurchaseOrder();
                po.TotalValue = 1;
                po.Customer = customer;

                po.Validate();
            });
            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
            Assert.Equal("ShippingAddress", ex.Message);
        }

        [Fact]
        public void OrderNotValidWhenOrderLinesMissing()
        {
            var ex = Record.Exception(() => {
                CustomerAddress address = new CustomerAddress("name", "street1", null, "zip", "city", "country");
                Customer customer = new Customer("first", "last");
                customer.Addresses.Add(address);

                PurchaseOrder po = new PurchaseOrder();
                po.TotalValue = 1;
                po.Customer = customer;
                po.ShippingAddress = address;

                po.Validate();
            });
            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
            Assert.Equal("OrderLines", ex.Message);
        }

        [Fact]
        public void OrderNotValidWhenTotalValueLessThanZero()
        {
            var ex = Record.Exception(() => {
                Product product = new Product("something", new BookProductType());
                CustomerAddress address = new CustomerAddress("name", "street1", null, "zip", "city", "country");
                Customer customer = new Customer("first", "last");
                customer.Addresses.Add(address);

                PurchaseOrder po = new PurchaseOrder();
                po.TotalValue = -1;
                po.Customer = customer;
                po.ShippingAddress = address;
                po.OrderLines.Add(new PurchaseOrderLine(product));

                po.Validate();
            });
            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
            Assert.Equal("TotalValue", ex.Message);
        }

        [Fact]
        public void OrderValidWhenAllDataPresentAndValid()
        {
            var ex = Record.Exception(() => {
                Product product = new Product("something", new BookProductType());
                CustomerAddress address = new CustomerAddress("name", "street1", null, "zip", "city", "country");
                Customer customer = new Customer("first", "last");
                customer.Addresses.Add(address);

                PurchaseOrder po = new PurchaseOrder();
                po.TotalValue = 1;
                po.Customer = customer;
                po.ShippingAddress = address;
                po.OrderLines.Add(new PurchaseOrderLine(product));

                po.Validate();
            });
            
            Assert.Null(ex);
        }
    }
}