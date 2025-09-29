namespace FoodDeliveryApp.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public decimal Amount { get; set; }
        public int PaymentTypeMasterId { get; set; }
        public virtual PaymentTypeMaster PaymentTypeMaster { get; set; }

        public int PaymentStatusMasterId { get; set; }
        public virtual PaymentStatusMaster PaymentStatusMaster { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}