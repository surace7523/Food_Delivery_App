namespace FoodDeliveryApp.Models
{
    public class PaymentTypeMaster
    {
        public int PaymentTypeMasterId { get; set; }
        public string TypeName { get; set; }  //Credit Card, PayPal, etc.
        public string? Description { get; set; }
        //Navigation
        public virtual ICollection<Payment> Payments { get; set; }

    }
}