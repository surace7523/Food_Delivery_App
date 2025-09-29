namespace FoodDeliveryApp.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string ItemName { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }


        public bool IsVeg { get; set; }

        public bool IsAvailable { get; set; }


        public string? ImageUrl { get; set; }

        //Category Relationship

        public int MenuCategoryId { get; set; }

        public virtual MenuCategory MenuCategory { get; set; }

        //Tracking rating for the item specifically
        //public double AverageRating { get; set; }
        //public int TotalReviews { get; set; }
    }
}