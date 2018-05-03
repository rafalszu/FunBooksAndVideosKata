using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using FunBooksAndVideos.Models;

namespace FunBooksAndVideos.Services
{
    // in a real world scenario this would be done using some messaging system like rabbitmq
    // and subscribers would be responsible for processing purhcase orders
    public class PurchaseOrderProcessingService : IPurchaseOrderProcessingService
    {
        private ConcurrentQueue<PurchaseOrder> _orderQueue;
        public ConcurrentQueue<PurchaseOrder> OrderQueue
        {
            get
            {
                if(_orderQueue == null)
                    _orderQueue = new ConcurrentQueue<PurchaseOrder>();

                return _orderQueue;
            }
        }

        public async Task ProcessPurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            if(purchaseOrder == null)
                throw new ArgumentNullException(nameof(purchaseOrder));

            if(purchaseOrder.OrderLines == null)
                throw new Exception("Can't process orders without lines");

            // schedule queue processing after 2 seconds
            await Task.Factory.StartNew(async () => await ProcessItemsInQueueAsync(2000));

            _orderQueue.Enqueue(purchaseOrder);
        }

        private async Task ProcessItemsInQueueAsync(int delayMs)
        {
            await Task.Delay(delayMs);
            
            // for the sake of simplicity this is just a simple loop, but should be parallelized
            while(OrderQueue.Count > 0)
            {
                PurchaseOrder order = null;
                if(OrderQueue.TryDequeue(out order))
                    await FinalizePurchaseOrderAsync(order);
            }
        }

        internal async Task FinalizePurchaseOrderAsync(PurchaseOrder order)
        {
            if(order == null)
                throw new ArgumentNullException(nameof(order));

            if(order.OrderLines == null)
                throw new Exception("Can't process orders without lines");

            // get all assemblies with interface IOrderProcessor
            // go line by line
            // check if any of assemblies CanProcess given productType
            // if so, process line
        }
    }
}