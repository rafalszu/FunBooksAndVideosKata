using System;
using FunBooksAndVideos.Models.Exceptions;
using FunBooksAndVideos.Models.Interfaces;

namespace FunBooksAndVideos.Models
{
    public class PurchaseOrderLine : ModelBase, IValidation
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

        public void Validate()
        {
            if(Product == null)
                throw new ValidationErrorException(nameof(Product));

            Product.Validate();
        }
    }
}