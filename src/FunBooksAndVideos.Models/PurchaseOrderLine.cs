using System;

namespace FunBooksAndVideos.Models
{
    public class PurchaseOrderLine : ModelBase
    {
        public Product Product { get; set; }

        public PurchaseOrderLine() : base()
        {}
    }
}