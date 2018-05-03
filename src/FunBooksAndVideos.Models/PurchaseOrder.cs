using System;
using System.Collections.Generic;
using System.Linq;
using FunBooksAndVideos.Models.Exceptions;
using FunBooksAndVideos.Models.Interfaces;

namespace FunBooksAndVideos.Models
{
    public class PurchaseOrder : ModelBase, IValidation
    {
        public string OrderNumber { get; private set; }
        public decimal TotalValue { get; set; }
        public Customer Customer { get; set; }
        public CustomerAddress ShippingAddress { get; set; }
        public List<PurchaseOrderLine> OrderLines { get; set; }
        public ShippingSlip ShippingSlip { get; set; }

        public PurchaseOrder() : base()
        { 
            this.OrderLines = new List<PurchaseOrderLine>();
            
            this.OrderNumber = AllocateOrderNumber();
        }

        public PurchaseOrder(decimal totalValue, Customer customer, CustomerAddress shippingAddress) : this()
        {
            if(customer == null)
                throw new ArgumentNullException(nameof(customer));
            if(shippingAddress == null)
                throw new ArgumentNullException(nameof(shippingAddress));
            if(totalValue < 0)
                throw new ArgumentOutOfRangeException("Unable to register negative purchase numbers");

            this.TotalValue = totalValue;
            this.Customer = customer;
            this.ShippingAddress = shippingAddress;
        }

        private string AllocateOrderNumber()
        {
            Random r = new Random();
            return r.Next(1, 999999).ToString("D6");
        }

        public void Validate()
        {
            if(string.IsNullOrWhiteSpace(OrderNumber))
                throw new ValidationErrorException(nameof(OrderNumber));
            if(TotalValue < 0)
                throw new ValidationErrorException(nameof(TotalValue));
            if(Customer == null)
                throw new ValidationErrorException(nameof(Customer));
            Customer.Validate();

            if(ShippingAddress == null)
                throw new ValidationErrorException(nameof(ShippingAddress));
            ShippingAddress.Validate();

            if(OrderLines == null || !OrderLines.Any())
                throw new ValidationErrorException(nameof(OrderLines));

            OrderLines.ForEach(o => o.Validate());
        }
    }
}