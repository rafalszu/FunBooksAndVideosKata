using System;
using System.Threading.Tasks;
using FunBooksAndVideos.Models;
using FunBooksAndVideos.Models.Exceptions;
using FunBooksAndVideos.Services.Processors;
using Xunit;

namespace FunBooksAndVideos.UnitTests
{
    public class PhysicalProductTypeProcessorTests
    {
        [Fact]
        public void CanProcessIPhysicalProductType()
        {
            PhysicalProductTypeProcessor proc = new PhysicalProductTypeProcessor();

            Product book = new Product("socme", new BookProductType());
            Assert.True(proc.CanProcess(book));

            Product video = new Product("asf", new VideoProductType());
            Assert.True(proc.CanProcess(video));
        }

        [Fact]
        public void FailsOnProcessingOtherProductTpes()
        {
            PhysicalProductTypeProcessor proc = new PhysicalProductTypeProcessor();

            Product bookMembership = new Product("book membership", new BookMembershipProductType());
            Assert.False(proc.CanProcess(bookMembership));
        }

        [Fact]
        public async Task CantProcessNullOrder()
        {
            PhysicalProductTypeProcessor proc = new PhysicalProductTypeProcessor();

            var ex = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(null, new PurchaseOrderLine());
            });
            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public async Task CantProcessNullOrderLine()
        {
            PhysicalProductTypeProcessor proc = new PhysicalProductTypeProcessor();

            var ex = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(new PurchaseOrder(), null);
            });
            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public async Task CantProcessInvalidOrder()
        {
            PhysicalProductTypeProcessor proc = new PhysicalProductTypeProcessor();

            var ex = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(new PurchaseOrder(), new PurchaseOrderLine());
            });
            Assert.NotNull(ex);
            Assert.IsType<ValidationErrorException>(ex);
        }

        [Fact]
        public async Task ProcessesOnlyMembershipTypes()
        {
            CustomerAddress addr = new CustomerAddress("name", "street1", null, "zip", "city", "country");
            Customer customer = new Customer("first", "last");
            customer.Addresses.Add(addr);

            Product book = new Product("book", new BookProductType());
            Product video = new Product("video", new VideoProductType());

            PhysicalProductTypeProcessor proc = new PhysicalProductTypeProcessor();
            
            PurchaseOrder validorder1 = new PurchaseOrder(1, customer, addr);
            PurchaseOrderLine validLine1 = new PurchaseOrderLine(book);
            validorder1.OrderLines.Add(validLine1);

            var ex1 = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(validorder1, validLine1);
            });
            Assert.Null(ex1);

            PurchaseOrder validorder2 = new PurchaseOrder(1, customer, addr);
            PurchaseOrderLine validLine2 = new PurchaseOrderLine(book);
            validorder2.OrderLines.Add(validLine2);

            var ex2 = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(validorder1, validLine2);
            });
            Assert.Null(ex2);
        }

        [Fact]
        public async Task ProcessingFailsWhenNotSupportedTypePassed()
        {
            CustomerAddress addr = new CustomerAddress("name", "street1", null, "zip", "city", "country");
            Customer customer = new Customer("first", "last");
            customer.Addresses.Add(addr);

            Product bookMembership = new Product("book membership", new BookMembershipProductType());

            PhysicalProductTypeProcessor proc = new PhysicalProductTypeProcessor();

            PurchaseOrder invalidOrder = new PurchaseOrder(1, customer, addr);
            PurchaseOrderLine invalidLine = new PurchaseOrderLine(bookMembership);
            invalidOrder.OrderLines.Add(invalidLine);

            var ex3 = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(invalidOrder, invalidLine);
            });
            Assert.NotNull(ex3);
            Assert.IsType<NotSupportedException>(ex3);
        }

        [Fact]
        public async Task ShipmentSlipNOTGeneratedWhenProductIsNotPhysical()
        {
            CustomerAddress addr = new CustomerAddress("name", "street1", null, "zip", "city", "country");
            Customer customer = new Customer("first", "last");
            customer.Addresses.Add(addr);

            Product bookMembership = new Product("book membership", new BookMembershipProductType());
            PhysicalProductTypeProcessor proc = new PhysicalProductTypeProcessor();

            PurchaseOrder validorder = new PurchaseOrder(1, customer, addr);
            PurchaseOrderLine validLine = new PurchaseOrderLine(bookMembership);
            validorder.OrderLines.Add(validLine);

            Assert.Null(validorder.ShippingSlip);

            var ex2 = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(validorder, validLine);
            });
            Assert.NotNull(ex2);
            Assert.IsType<NotSupportedException>(ex2);
        }

        [Fact]
        public async Task ShipmentSlipGetsGeneratedForPhysicalProductType()
        {
            CustomerAddress addr = new CustomerAddress("name", "street1", null, "zip", "city", "country");
            Customer customer = new Customer("first", "last");
            customer.Addresses.Add(addr);

            Product book = new Product("book", new BookProductType());
            PhysicalProductTypeProcessor proc = new PhysicalProductTypeProcessor();

            PurchaseOrder validorder = new PurchaseOrder(1, customer, addr);
            PurchaseOrderLine validLine = new PurchaseOrderLine(book);
            validorder.OrderLines.Add(validLine);

            Assert.Null(validorder.ShippingSlip);

            var ex2 = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(validorder, validLine);
            });
            Assert.Null(ex2);
            Assert.NotNull(validorder.ShippingSlip);
        }
    }
}