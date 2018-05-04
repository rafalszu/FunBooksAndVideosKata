using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private readonly List<IProcessor> processors;
        public List<IProcessor> Processors
        {
            get { return processors; }
        }

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
            processors = new List<IProcessor>();

            var entryAssembly = System.Reflection.Assembly.GetExecutingAssembly();//.GetEntryAssembly();
            foreach(var definedType in entryAssembly.DefinedTypes)
            {
                if(definedType.ImplementedInterfaces.Contains(typeof(IProcessor)) && !definedType.IsInterface)
                    processors.Add((IProcessor)entryAssembly.CreateInstance(definedType.FullName));
            }
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

            var tasks = order.OrderLines.Select(async line => 
            {
                await RunProcessorsAsync(order, line);
            });
            
            await Task.WhenAll(tasks);
        }

        private async Task RunProcessorsAsync(PurchaseOrder order, PurchaseOrderLine line)
        {
            if(order == null)
                throw new ArgumentNullException(nameof(order));
            if(line == null)
                throw new ArgumentNullException(nameof(line));

            var tasks = processors.Select(async proc => 
            {
                if(proc.CanProcess(line.Product))
                    await proc.ProcessAsync(order, line);
            });

            await Task.WhenAll(tasks);
        }
    }
}