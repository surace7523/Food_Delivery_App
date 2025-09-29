namespace FoodDeliveryApp.Models
{
    public class Offer
    {
        public int OfferId { get; set; }
        public string OfferCode { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
