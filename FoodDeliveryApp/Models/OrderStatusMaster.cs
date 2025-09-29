namespace FoodDeliveryApp.Models
{
    public class OrderStatusMaster
    {

        public int OrderStatusMasterId { get; set; }
        public string StatusName { get; set; }  //placed confirmed confirming 
        public string? Description { get; set; }

        //Navigation
        public virtual ICollection<Order> Orders { get; set; }
    }
}
