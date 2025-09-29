namespace FoodDeliveryApp.Models
{
    public class DeliveryStatusMaster
    {
        public int DeliveryStatusMasterId { get; set; }
        public string StatusName { get; set; }  //out for delivery, delivered, delayed
        public string? Description { get; set; }
        //Navigation
        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
