namespace FoodDeliveryApp.Models
{
    public class OrderOffer
    {
        public int OrderOfferId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; }

        //ACTUAL discount amount applied from this offer
        public decimal DiscountApplied { get; set; }
    }
}
