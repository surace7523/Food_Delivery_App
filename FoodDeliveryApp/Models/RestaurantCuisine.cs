namespace FoodDeliveryApp.Models
{
    public class RestaurantCuisine
    {

        //Foreign key to Restaurant
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        //Cuisine type e.g., Italian, Chinese, Indian
        public int CuisineId { get; set; }
        public virtual Cuisine Cuisine { get; set; }

    }
}