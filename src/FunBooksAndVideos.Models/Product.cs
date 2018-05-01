using System;

namespace FunBooksAndVideos.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }

        public Product() => this.Id = Guid.NewGuid();

        public Product(string name, ProductType productType) : this()
        {
            this.Name = name;
            this.Type = productType;
        }
    }
}