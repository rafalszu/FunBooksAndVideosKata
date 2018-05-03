namespace FunBooksAndVideos.Models
{
    public class ShippingSlip : ModelBase
    {
        public string RelatedOrderNumber { get; private set; }

        public ShippingSlip() : base() {}

        public ShippingSlip(string orderNumber) : this()
        {
            this.RelatedOrderNumber = orderNumber;
        }
    }
}