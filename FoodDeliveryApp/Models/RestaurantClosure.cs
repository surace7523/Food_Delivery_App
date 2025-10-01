namespace FoodDeliveryApp.Models
{
    public class RestaurantClosure
    {
        public int RestaurantClosureId { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string? Reason { get; set; } //e.g., maintenance, holiday, etc.
    }
}