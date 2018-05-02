using System;

namespace FunBooksAndVideos.Models
{
    public class PurchaseOrderLine : ModelBase
    {
        public Product Product { get; set; }

        public PurchaseOrderLine() : base()
        {}

        public PurchaseOrderLine(Product product) : this() 
        {
            if(product == null)
                throw new ArgumentNullException(nameof(product));
                 
            this.Product = product;
        }
    }
}