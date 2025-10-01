namespace FoodDeliveryApp.Models
{
    public class BankDetails
    {
        public int BankDetailsId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        //possible bank details for payments
        public string? AccountHolderName { get; set; } //making it nullable
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSCCode { get; set; }
    }
}
