namespace FoodDeliveryApp.Models
{
    /// <summary>
    /// A master table for cuisines like Indian, chines, Mexican
    /// </summary>
    public class Cuisine
    {
        public int CuisineId { get; set; }
        public string CuisineName { get; set; }
        public string CuisineDescription { get; set; }

        //for icon and image of teh food if needed

        public string? ImageUrl { get; set; }


        //navigation one cousine can have multiple restaurant

        public virtual ICollection<RestaurantCuisine> RestaurantCuisines { get; set; }
        public bool IsActive { get; internal set; }
    }
}
