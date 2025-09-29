namespace FoodDeliveryApp.Models
{
    public class UserAddress
    {
        public int UserAddressId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; } //making it nullable
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public bool IsDefault { get; set; } //to mark default address
        //Audit information
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}