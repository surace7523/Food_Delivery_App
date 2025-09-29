using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }

        //foreign key to the owner if needed 

        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
         public bool IsApproved { get; set; } //Admin approval status for listing the restairant

        //store operation times
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }

        //cpuld store agregate review rating
        [NotMapped]  //will be a property but wont be a column in db 
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }



        //Navigation

        public virtual ICollection<RestaurantCuisine> RestaurantCuisines { get; set; }
        public virtual ICollection<MenuCategory> MenuCategories { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    
    }
}
