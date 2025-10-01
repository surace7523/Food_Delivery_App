namespace FoodDeliveryApp.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }
        public  Order Order { get; set; }

        //The user who is delivering the order(Delivery Partner)
        public int DeliveryPartnerProfileId { get; set; }
        public virtual DeliveryPartnerProfile DeliveryPartnerProfile { get; set; }

        public DateTime? PickupTime { get; set; }
        public DateTime?DeliveryTime { get; set; }

        public int DeliveryStatusMasterId { get; set; }

        public virtual DeliveryStatusMaster DeliveryStatusMaster { get; set; }
    }
}