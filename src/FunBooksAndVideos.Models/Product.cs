using System;
using FunBooksAndVideos.Models.Exceptions;
using FunBooksAndVideos.Models.Interfaces;

namespace FunBooksAndVideos.Models
{
    public class Product : ModelBase, IValidation
    {
        public string Name { get; set; }
        public IProductType Type { get; set; }

        public Product() : base()
        {}

        public Product(string name, IProductType productType) : this()
        {
            if(productType == null)
                throw new ArgumentNullException(nameof(productType));

            this.Name = name;
            this.Type = productType;
        }

        public void Validate()
        {
            if(string.IsNullOrWhiteSpace(Name))
                throw new ValidationErrorException(nameof(Name));
            if(Type == null)
                throw new ValidationErrorException(nameof(Type));
        }
    }
}