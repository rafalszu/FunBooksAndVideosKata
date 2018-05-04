using System;
using System.Threading.Tasks;
using FunBooksAndVideos.Models;

namespace FunBooksAndVideos.Services.Processors
{
    public class PhysicalProductTypeProcessor : IProcessor
    {
        public bool CanProcess(IPhysicalProductType productType)
        {
            return true;
        }

        public bool CanProcess(Product product)
        {
            return product.Type is IPhysicalProductType;
        }

        public async Task ProcessAsync(PurchaseOrder order, PurchaseOrderLine line)
        {
            if(order == null)
                throw new ArgumentNullException(nameof(order));
            if(line == null)
                throw new ArgumentNullException(nameof(line));
            if(line.Product == null)
                throw new ArgumentNullException(nameof(line.Product));
            if(line.Product.Type == null)
                throw new ArgumentNullException(nameof(line.Product.Type));
            
            await Task.Factory.StartNew(() => 
            {
                ShippingSlip shippingSlip = new ShippingSlip(order.OrderNumber);
                order.ShippingSlip = shippingSlip;
            });
        }
    }
}