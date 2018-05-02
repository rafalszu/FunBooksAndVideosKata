using System;
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
        { 
            this.OrderNumber = AllocateOrderNumber();
        }

        public PurchaseOrder(decimal totalValue, Customer customer, CustomerAddress shippingAddress) : this()
        {
            if(customer == null)
                throw new ArgumentNullException(nameof(customer));
            if(shippingAddress == null)
                throw new ArgumentNullException(nameof(shippingAddress));

            this.TotalValue = totalValue;
            this.Customer = customer;
            this.ShippingAddress = shippingAddress;
        }

        private string AllocateOrderNumber()
        {
            Random r = new Random();
            return r.Next(1, 999999).ToString("D6");
        }
    }
}