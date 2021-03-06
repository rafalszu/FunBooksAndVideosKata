using System;
using System.Threading.Tasks;
using FunBooksAndVideos.Models;

namespace FunBooksAndVideos.Services.Processors
{
    public class MembershipTypeProcessor : IProcessor
    {
        public bool CanProcess(Product product)
        {
            return product.Type is IMembershipProductType;
        }

        public async Task ProcessAsync(PurchaseOrder order, PurchaseOrderLine line)
        {
            if(order == null)
                throw new ArgumentNullException(nameof(order));
            if(line == null)
                throw new ArgumentNullException(nameof(line));
            
            order.Validate();
            if(!CanProcess(line.Product))
                throw new NotSupportedException(nameof(line.Product));
            
            await Task.Factory.StartNew(() => 
            {
                // order membership type if necessary
                if(order.Customer.MembershipType == CustomerMembershipType.None) 
                {
                    // set to the one from order
                    order.Customer.MembershipType = ((IMembershipProductType)line.Product.Type).MembershipType;
                }
                else if(order.Customer.MembershipType != ((IMembershipProductType)line.Product.Type).MembershipType) 
                {
                    // set as premium
                    order.Customer.MembershipType = CustomerMembershipType.Premium;
                }
            });
        }
    }
}