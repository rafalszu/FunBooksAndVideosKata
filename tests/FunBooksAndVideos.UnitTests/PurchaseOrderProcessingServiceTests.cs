using System;
using System.Threading.Tasks;
using FunBooksAndVideos.Models;
using FunBooksAndVideos.Models.Exceptions;
using FunBooksAndVideos.Services;
using Xunit;

namespace FunBooksAndVideos.UnitTests
{
    public class PurchaseOrderProcessingServiceTests
    {
        [Fact]
        public void ImplementsIPurchaseOrderProcessingServiceInterface()
        {
            Assert.True(typeof(IPurchaseOrderProcessingService).IsAssignableFrom(typeof(PurchaseOrderProcessingService)));
        }

        [Fact]
        public async Task CantProcessNullOrder()
        {
            PurchaseOrderProcessingService service = new PurchaseOrderProcessingService();
            var exception = await Record.ExceptionAsync(async () => {
                await service.ProcessPurchaseOrderAsync(null);
            });
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        // [Fact]
        // public async Task CantProcessOrderWithoutLines()
        // {
        //     PurchaseOrderProcessingService service = new PurchaseOrderProcessingService();
        //     var exception = await Record.ExceptionAsync(async () => {
        //         PurchaseOrder order = new PurchaseOrder();
        //         await service.ProcessPurchaseOrderAsync(order);
        //     });
        //     Assert.NotNull(exception);
        //     Assert.IsType<ValidationErrorException>(exception);
        // }
    }
}