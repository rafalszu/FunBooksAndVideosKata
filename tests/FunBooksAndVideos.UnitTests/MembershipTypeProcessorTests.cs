using System;
using System.Threading.Tasks;
using FunBooksAndVideos.Models;
using FunBooksAndVideos.Models.Exceptions;
using FunBooksAndVideos.Services.Processors;
using Xunit;

namespace FunBooksAndVideos.UnitTests
{
    public class MembershipTypeProcessorTests
    {
        [Fact]
        public void CanProcessIMembershipType()
        {
            MembershipTypeProcessor proc = new MembershipTypeProcessor();

            Product bookMembership = new Product("socme", new BookMembershipProductType());
            Assert.True(proc.CanProcess(bookMembership));

            Product videoMembership = new Product("asf", new VideoMembershipProductType());
            Assert.True(proc.CanProcess(videoMembership));
        }

        [Fact]
        public void FailsOnProcessingOtherProductTypes()
        {
            MembershipTypeProcessor proc = new MembershipTypeProcessor();

            Product video = new Product("asf", new VideoProductType());
            Assert.False(proc.CanProcess(video));
        }

        [Fact]
        public async Task CantProcessNullOrder()
        {
            MembershipTypeProcessor proc = new MembershipTypeProcessor();

            var ex = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(null, new PurchaseOrderLine());
            });
            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public async Task CantProcessNullOrderLine()
        {
            MembershipTypeProcessor proc = new MembershipTypeProcessor();

            var ex = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(new PurchaseOrder(), null);
            });
            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public async Task CantProcessInvalidOrder()
        {
            MembershipTypeProcessor proc = new MembershipTypeProcessor();

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

            Product bookMembership = new Product("book membership", new BookMembershipProductType());
            Product videoMembership = new Product("video membership", new VideoMembershipProductType());
            Product book = new Product("a book", new BookProductType());

            MembershipTypeProcessor proc = new MembershipTypeProcessor();
            
            PurchaseOrder validorder1 = new PurchaseOrder(1, customer, addr);
            PurchaseOrderLine validLine1 = new PurchaseOrderLine(bookMembership);
            validorder1.OrderLines.Add(validLine1);

            var ex1 = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(validorder1, validLine1);
            });
            Assert.Null(ex1);

            PurchaseOrder validorder2 = new PurchaseOrder(1, customer, addr);
            PurchaseOrderLine validLine2 = new PurchaseOrderLine(bookMembership);
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
            Product videoMembership = new Product("video membership", new VideoMembershipProductType());
            Product book = new Product("a book", new BookProductType());

            MembershipTypeProcessor proc = new MembershipTypeProcessor();

            PurchaseOrder invalidOrder = new PurchaseOrder(1, customer, addr);
            PurchaseOrderLine invalidLine = new PurchaseOrderLine(book);
            invalidOrder.OrderLines.Add(invalidLine);

            var ex3 = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(invalidOrder, invalidLine);
            });
            Assert.NotNull(ex3);
            Assert.IsType<NotSupportedException>(ex3);
        }

        [Fact]
        public async Task MembershipChangesToBookmembershipWhenWasNoneAndBookmembershipWasOrdered()
        {
            CustomerAddress addr = new CustomerAddress("name", "street1", null, "zip", "city", "country");
            Customer customer = new Customer("first", "last");
            customer.Addresses.Add(addr);

            Product bookMembership = new Product("book membership", new BookMembershipProductType());
            PurchaseOrder validorder = new PurchaseOrder(1, customer, addr);
            PurchaseOrderLine validLine = new PurchaseOrderLine(bookMembership);
            validorder.OrderLines.Add(validLine);

            MembershipTypeProcessor proc = new MembershipTypeProcessor();

            var ex = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(validorder, validLine);
            });

            Assert.Null(ex);
            Assert.Equal(CustomerMembershipType.BookClub, customer.MembershipType);
        }

        [Fact]
        public async Task MembershipChangesToPremiumWhenWasVideomembershipAndBookmembershipWasOrdered()
        {
            CustomerAddress addr = new CustomerAddress("name", "street1", null, "zip", "city", "country");
            Customer customer = new Customer("first", "last");
            customer.Addresses.Add(addr);
            customer.MembershipType = CustomerMembershipType.VideoClub;

            Product bookMembership = new Product("book membership", new BookMembershipProductType());
            PurchaseOrder validorder = new PurchaseOrder(1, customer, addr);
            PurchaseOrderLine validLine = new PurchaseOrderLine(bookMembership);
            validorder.OrderLines.Add(validLine);

            MembershipTypeProcessor proc = new MembershipTypeProcessor();

            var ex = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(validorder, validLine);
            });

            Assert.Null(ex);
            Assert.Equal(CustomerMembershipType.Premium, customer.MembershipType);
        }

        [Fact]
        public async Task MembershipDoesntChangeWhenWasBookMembershipAndBookmembershipWasOrdered()
        {
            CustomerAddress addr = new CustomerAddress("name", "street1", null, "zip", "city", "country");
            Customer customer = new Customer("first", "last");
            customer.Addresses.Add(addr);
            customer.MembershipType = CustomerMembershipType.BookClub;

            Product bookMembership = new Product("book membership", new BookMembershipProductType());
            PurchaseOrder validorder = new PurchaseOrder(1, customer, addr);
            PurchaseOrderLine validLine = new PurchaseOrderLine(bookMembership);
            validorder.OrderLines.Add(validLine);

            MembershipTypeProcessor proc = new MembershipTypeProcessor();

            var ex = await Record.ExceptionAsync(async () => {
                await proc.ProcessAsync(validorder, validLine);
            });

            Assert.Null(ex);
            Assert.Equal(CustomerMembershipType.BookClub, customer.MembershipType);
        }
    }
}