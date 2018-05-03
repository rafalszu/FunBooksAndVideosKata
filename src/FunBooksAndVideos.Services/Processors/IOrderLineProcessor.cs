using FunBooksAndVideos.Models;

namespace FunBooksAndVideos.Services.Processors
{
    public interface IOrderLineProcessor<T> where T : IProductType
    {
        bool CanProcess(T productType);

        void Process(PurchaseOrder order, PurchaseOrderLine line);
    }
}