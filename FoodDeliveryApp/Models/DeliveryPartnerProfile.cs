using System.ComponentModel.DataAnnotations.Schema;
using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Models
{
    public class DeliveryPartnerProfile
    {
        public int DeliveryPartnerProfileId { get; set; }
        //1 :1 relationshop with User
        public int UserId { get; set; }
        public virtual User User { get; set; }


        //Role specific field for delivery partner
        public string VehicleType { get; set; } //e.g., bike, car, scooter
        public string LicenseNumber { get; set; }
        public string VehicleRegistrationNumber { get; set; }

        //Navigation to deliveries
        public virtual ICollection<Delivery> Deliveries { get; set; }


        //This will dynamically count te number of completed deliveries without storing in db.
        public int TotalDeliveries => Deliveries?.Count ?? 0;


        //navigation for reviews given to the delivery partner
        public virtual ICollection<Review> Reviews { get; set; }

        [NotMapped]
        public decimal AverageRating => Reviews != null && Reviews.Count > 0 ? (decimal)Reviews.Average(r => r.Rating) : 0;
    }
}

