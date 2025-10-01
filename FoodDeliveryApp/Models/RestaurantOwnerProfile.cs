namespace FoodDeliveryApp.Models
{
    public class RestaurantOwnerProfile
    {
        public int RestaurantOwnerProfileId { get; set; }

        //1 :1 relationshop with User
        public int UserId { get; set; }
        public virtual User User{ get; set; }


        //Role specific field for restaurant owner
        public string BusinessLiscenceNumber { get; set; }
        public string BusinessRegistrationNumber { get; set; }
        public bool IsVerified { get; set; }  //admin will verify if u are a legit business
        public string? AdminRemarks { get; set; } //nullable field for admin to leave remarks during verification



    }
}