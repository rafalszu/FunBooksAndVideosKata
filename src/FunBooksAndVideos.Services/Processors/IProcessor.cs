using System.Threading.Tasks;
using FunBooksAndVideos.Models;

namespace FunBooksAndVideos.Services.Processors
{
    public interface IProcessor
    {
        bool CanProcess(Product product);

        Task ProcessAsync(PurchaseOrder order, PurchaseOrderLine line);
    }
}