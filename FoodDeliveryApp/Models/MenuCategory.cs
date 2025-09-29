namespace FoodDeliveryApp.Models
{
    public class MenuCategory
    {
        public int MenuCategoryId { get; set; }
        public string CategoryName { get; set; }

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        //Restaurant Relationship

        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get;  set; }  



        //Navigation

        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}