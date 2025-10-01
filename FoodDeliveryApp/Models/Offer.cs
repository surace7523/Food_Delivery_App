namespace FoodDeliveryApp.Models
{
    public class Offer
    {
        public int OfferId { get; set; }
        public string OfferCode { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }

        public decimal DiscountPercentage { get; set; } //if percentage based discount

        public bool IsPercentage { get; set; } //true if percentage based, false if fixed amount
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //offer scope
        public bool IsGlobal { get; set; } //if true, applicable to all users

        //nullable foreign key for restaurant specific offers
        public int? RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        public bool IsActive { get; set; } //to activate or deactivate offers

      
    }
}
