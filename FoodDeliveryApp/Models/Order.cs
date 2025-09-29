namespace FoodDeliveryApp.Models
{
    public class Order
    {

        public int OrderId { get; set; }

        //Customer who placed the order
        public int CustomerId { get; set; }
        public virtual User Customer { get; set; }

        //Relationshopt to Restaurant
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        //which address (UserAddress the user selected for delivery)
        public int UserAddressId { get; set; }
        public virtual UserAddress UserAddress { get; set; }

        //Timestamp
        public DateTime OrderDate { get; set; }

        //status of the order
        public int OrderStatusMasterId { get; set; }
        public virtual OrderStatusMaster OrderStatusMaster { get; set; }



        //Totals

        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Discount { get; set; }


        //Additional notes from customer
        public string? CustomerNotes { get; set; }

        //OrderItems
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        //Payment Information
        public virtual Payment Payment { get; set; }

        //Delivery info
        public virtual Delivery Delivery { get; set; }

    }
}