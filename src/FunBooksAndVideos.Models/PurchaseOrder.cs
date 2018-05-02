using System.Collections.Generic;

namespace FunBooksAndVideos.Models
{
    public class PurchaseOrder : ModelBase
    {
        public string OrderNumber { get; set; }
        public decimal TotalValue { get; set; }
        public Customer Customer { get; set; }
        public CustomerAddress ShippingAddress { get; set; }
        List<PurchaseOrderLine> OrderLines { get; set; }

        public PurchaseOrder() : base()
        {}
    }
}