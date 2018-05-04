using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using FunBooksAndVideos.Models;
using FunBooksAndVideos.Models.Exceptions;
using FunBooksAndVideos.Services.Processors;

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

        public PurchaseOrderProcessingService()
        {
            // get all assemblies with interface IOrderProcessor
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IOrderLineProcessor<>).IsAssignableFrom(p) && !p.IsInterface)
                .ToList();

        }

        public async Task ProcessPurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            if(purchaseOrder == null)
                throw new ArgumentNullException(nameof(purchaseOrder));
            if(purchaseOrder.OrderLines == null)
                throw new ValidationErrorException("Can't process orders without lines");
            
            OrderQueue.Enqueue(purchaseOrder);
            // in real world scenario dequeue would be timer based, bur for the sake of the simplicity i'll just call the dequeue manually
            await ProcessItemsInQueueAsync();
        }

        private async Task ProcessItemsInQueueAsync()
        {
            System.Console.WriteLine("Queue count: {0}", OrderQueue.Count);
            // for the sake of simplicity this is just a simple loop, but should be parallelized
            while(OrderQueue.Count > 0)
            {
                PurchaseOrder order = null;
                if(OrderQueue.TryDequeue(out order))
                {
                    await FinalizePurchaseOrderAsync(order);
                }
            }
        }

        internal async Task FinalizePurchaseOrderAsync(PurchaseOrder order)
        {
            if(order == null)
                throw new ArgumentNullException(nameof(order));
            order.Validate();

            
            // types.ForEach(t => {
            //     // create instance of given type
            //     var instance = Activator.CreateInstance(t);
                
            //     // check canHandle
            //     // if so, call handle
            // });
        }
    }
}