using System;
using FunBooksAndVideos.Models;

namespace FunBooksAndVideos.Services.Processors
{
    public class MembershipTypeProcessor : IOrderLineProcessor<IMembershipProductType>
    {
        public bool CanProcess(IMembershipProductType productType)
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
            
            // order membership type if necessary
            if(order.Customer.MembershipType == CustomerMembershipType.None) 
            {
                // set to the one from order
                order.Customer.MembershipType = ((IMembershipProductType)line.Product.Type).MembershipType;
            }
            else
                // set as premium
                order.Customer.MembershipType = CustomerMembershipType.Premium;
        }
    }
}