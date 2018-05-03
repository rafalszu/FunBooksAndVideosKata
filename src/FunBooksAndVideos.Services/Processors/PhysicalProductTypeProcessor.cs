using System;
using FunBooksAndVideos.Models;

namespace FunBooksAndVideos.Services.Processors
{
    public class PhysicalProductTypeProcessor : IOrderLineProcessor<IPhysicalProductType>
    {
        public bool CanProcess(IPhysicalProductType productType)
        {
            return true;
        }

        public void Process(PurchaseOrder order, PurchaseOrderLine line)
        {
            if(order == null)
                throw new ArgumentNullException(nameof(order));
            if(line == null)
                throw new ArgumentNullException(nameof(line));
            if(line.Product == null)
                throw new ArgumentNullException(nameof(line.Product));
            if(line.Product.Type == null)
                throw new ArgumentNullException(nameof(line.Product.Type));
            
            ShippingSlip shippingSlip = new ShippingSlip(order.OrderNumber);
            order.ShippingSlip = shippingSlip;
        }
    }
}