using System;

namespace FunBooksAndVideos.Models
{
    public class Product : ModelBase
    {
        public string Name { get; set; }
        public ProductType Type { get; set; }

        public Product() : base()
        {}

        public Product(string name, ProductType productType) : base()
        {
            if(productType == null)
                throw new ArgumentNullException(nameof(productType));

            this.Name = name;
            this.Type = productType;
        }
    }
}