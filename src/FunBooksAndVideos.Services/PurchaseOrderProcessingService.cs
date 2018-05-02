using System.Collections.Concurrent;
using System.Threading.Tasks;
using FunBooksAndVideos.Models;

namespace FunBooksAndVideos.Services
{
    public class PurchaseOrderProcessingService : IPurchaseOrderProcessingService
    {
        private ConcurrentQueue<PurchaseOrder> _orderQueue;
        ConcurrentQueue<PurchaseOrder> OrderQueue
        {
            get
            {
                if(_orderQueue == null)
                    _orderQueue = new ConcurrentQueue<PurchaseOrder>();

                return _orderQueue;
            }
        }

        public Task ProcessPurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            return Task.Factory.StartNew(() => {
                _orderQueue.Enqueue(purchaseOrder);
            });
        }
    }
}