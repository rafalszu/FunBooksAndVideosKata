
using System.Threading.Tasks;
using FunBooksAndVideos.Models;

namespace FunBooksAndVideos.Services
{
    public interface IPurchaseOrderProcessingService
    {
        Task ProcessPurchaseOrderAsync(PurchaseOrder purchaseOrder);
    }
}