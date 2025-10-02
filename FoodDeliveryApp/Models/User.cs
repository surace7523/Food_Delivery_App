using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public bool IsEmailVerified { get; set; }
        public bool  IsPhoneNumberVerified { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public bool IsActive { get; set; }

        //role information
        public int RoleMasterId { get; set; }
        public virtual RoleMaster RoleMaster { get; set; }



        //Audit information

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }


        //navigation

        public virtual ICollection<UserAddress> UserAddress { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


        //optional one to one relationship to specialized profiles
        public virtual RestaurantOwnerProfile? RestaurantOwnerProfile { get; set; }
        public virtual DeliveryPartnerProfile? DeliveryPartnerProfile { get; set; }

    }
}