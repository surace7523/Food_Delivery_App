using FoodDeliveryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        // DbSets for your entities

        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryStatusMaster> DeliveryStatusMasters { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatusMaster> OrderStatusMasters { get; set; }
        public DbSet<RestaurantAddress> RestaurantAddresss { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentStatusMaster> PaymentStatusMasters { get; set; }
        public DbSet<PaymentTypeMaster> PaymentTypeMasters { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<User> Users { get; set; }
     
        public DbSet<OrderOffer> OrderOffers { get; set; }
        public DbSet<RestaurantClosure> RestaurantClosures { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<RestaurantOwnerProfile> RestaurantOwnerProfiles { get; set; }
        public DbSet<DeliveryPartnerProfile> DeliveryPartnerProfiles { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<RestaurantCuisine> RestaurantCuisines { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<BankDetails> BankDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure entity relationships and constraints here if needed

            //configure composite keys for many-to-many bridging tables
            modelBuilder.Entity<RestaurantCuisine>()
                .HasKey(rc => new { rc.RestaurantId, rc.CuisineId });

            //configure relationships to prevent multiple cascade paths
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);   //prevent cascade deletion 


            modelBuilder.Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);   //prevent cascade deletion

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Order)
                .WithOne(o => o.Delivery)
                .HasForeignKey<Delivery>(d => d.OrderId)
                .OnDelete(DeleteBehavior.Restrict);   //prevent cascade deletion

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.DeliveryPartnerProfile)
                .WithMany(u => u.Deliveries)   // plural = many
                .HasForeignKey(d => d.DeliveryPartnerProfileId)
                .OnDelete(DeleteBehavior.Restrict);   //prevent cascade deletion

            modelBuilder.Entity<Order>()
                .HasOne(o => o.UserAddress)
                .WithMany()
                .HasForeignKey(o => o.UserAddressId)
                .OnDelete(DeleteBehavior.Restrict);   //prevent cascade deletion    

            //masertable data seeding

            //.1 Rolemaster

            modelBuilder.Entity<RoleMaster>().HasData(
                new RoleMaster
                {
                    RoleMasterId = 1,
                    RoleName = "SuperAdmin",
                    Description = "SuperAdmin",
                    IsActive = true
                },
                new RoleMaster
                {
                    RoleMasterId = 2,
                    RoleName = "Admin",
                    Description = "Application administrator with full access",
                    IsActive = true
                },
                new RoleMaster
                {
                    RoleMasterId = 3,
                    RoleName = "Customer",
                    Description = "End user placing orders",
                    IsActive = true
                },
                new RoleMaster
                {
                    RoleMasterId = 4,
                    RoleName = "DeliveryPartner",
                    Description = "Partner responsible for delivering orders",
                    IsActive = true
                },
                new RoleMaster
                {
                    RoleMasterId = 5,
                    RoleName = "RestaurantOwner",
                    Description = "RestaurantOwner",
                    IsActive = true
                }
            );



            //202. OrderStatusMaster

            modelBuilder.Entity<OrderStatusMaster>().HasData(
                new OrderStatusMaster
                {
                    OrderStatusMasterId = 1,
                    StatusName = "Placed",
                    Description = "Order has been placed by the customer",
                    IsActive = true
                },
                new OrderStatusMaster
                {
                    OrderStatusMasterId = 2,
                    StatusName = "Confirmed",
                    Description = "Order has been confirmed by the restaurant",
                    IsActive = true
                },
                new OrderStatusMaster
                {
                    OrderStatusMasterId = 3,
                    StatusName = "Preparing",
                    Description = "Order is being prepared",
                    IsActive = true
                },
                new OrderStatusMaster
                {
                    OrderStatusMasterId = 4,
                    StatusName = "ReadyForPickup",
                    Description = "Order is ready for pickup by delivery partner",
                    IsActive = true
                },
                new OrderStatusMaster
                {
                    OrderStatusMasterId = 5,
                    StatusName = "OutForDelivery",
                    Description = "Order is on the way to customer",
                    IsActive = true
                },
                new OrderStatusMaster
                {
                    OrderStatusMasterId = 6,
                    StatusName = "Delivered",
                    Description = "Order has been delivered successfully",
                    IsActive = true
                },
                new OrderStatusMaster
                {
                    OrderStatusMasterId = 7,
                    StatusName = "Cancelled",
                    Description = "Order has been cancelled",
                    IsActive = true
                }
            );


            //3. DeliveryStatusMaster

            modelBuilder.Entity<DeliveryStatusMaster>().HasData(
                new DeliveryStatusMaster
                {
                    DeliveryStatusMasterId = 1,
                    StatusName = "OutForDelivery",
                    Description = "Order is out for delivery",
                    IsActive = true
                },
                new DeliveryStatusMaster
                {
                    DeliveryStatusMasterId = 2,
                    StatusName = "Delivered",
                    Description = "Order has been successfully delivered",
                    IsActive = true
                },
                new DeliveryStatusMaster
                {
                    DeliveryStatusMasterId = 3,
                    StatusName = "Delayed",
                    Description = "Delivery is delayed due to unforeseen reasons",
                    IsActive = true
                },
                new DeliveryStatusMaster
                {
                    DeliveryStatusMasterId = 4,
                    StatusName = "Failed",
                    Description = "Delivery attempt failed",
                    IsActive = true
                },
                new DeliveryStatusMaster
                {
                    DeliveryStatusMasterId = 5,
                    StatusName = "Returned",
                    Description = "Order was returned to the restaurant",
                    IsActive = true
                }
            );



            modelBuilder.Entity<PaymentStatusMaster>().HasData(
                new PaymentStatusMaster
                {
                    PaymentStatusMasterId = 1,
                    StatusName = "Pending",
                    Description = "Payment is pending and awaiting confirmation",
                    IsActive = true
                },
                new PaymentStatusMaster
                {
                    PaymentStatusMasterId = 2,
                    StatusName = "Completed",
                    Description = "Payment completed successfully",
                    IsActive = true
                },
                new PaymentStatusMaster
                {
                    PaymentStatusMasterId = 3,
                    StatusName = "Failed",
                    Description = "Payment failed due to an error",
                    IsActive = true
                },
                new PaymentStatusMaster
                {
                    PaymentStatusMasterId = 4,
                    StatusName = "Refunded",
                    Description = "Payment was refunded to the customer",
                    IsActive = true
                },
                new PaymentStatusMaster
                {
                    PaymentStatusMasterId = 5,
                    StatusName = "Cancelled",
                    Description = "Payment was cancelled before completion",
                    IsActive = true
                }
            );


            modelBuilder.Entity<PaymentTypeMaster>().HasData(
                new PaymentTypeMaster
                {
                    PaymentTypeMasterId = 1,
                    TypeName = "CreditCard",
                    Description = "Payment made using a credit card",
                    IsActive = true
                },
                new PaymentTypeMaster
                {
                    PaymentTypeMasterId = 2,
                    TypeName = "DebitCard",
                    Description = "Payment made using a debit card",
                    IsActive = true
                },
                new PaymentTypeMaster
                {
                    PaymentTypeMasterId = 3,
                    TypeName = "PayPal",
                    Description = "Payment made using PayPal",
                    IsActive = true
                },
                new PaymentTypeMaster
                {
                    PaymentTypeMasterId = 4,
                    TypeName = "CashOnDelivery",
                    Description = "Payment made in cash at the time of delivery",
                    IsActive = true
                },
                new PaymentTypeMaster
                {
                    PaymentTypeMasterId = 5,
                    TypeName = "UPI",
                    Description = "Payment made using UPI (Unified Payments Interface)",
                    IsActive = true
                },
                new PaymentTypeMaster
                {
                    PaymentTypeMasterId = 6,
                    TypeName = "Wallet",
                    Description = "Payment made using a digital wallet",
                    IsActive = true
                }
            );
            modelBuilder.Entity<Cuisine>().HasData(
            new Cuisine
            {
                CuisineId = 1,
                CuisineName = "Italian",
                CuisineDescription = "Pasta, pizza, and other classic Italian dishes",
                ImageUrl = "/images/cuisines/italian.png",
                IsActive = true
            },
            new Cuisine
            {
                CuisineId = 2,
                CuisineName = "Chinese",
                CuisineDescription = "Noodles, dumplings, stir-fries, and regional Chinese flavors",
                ImageUrl = "/images/cuisines/chinese.png",
                IsActive = true
            },
            new Cuisine
            {
                CuisineId = 3,
                CuisineName = "Indian",
                CuisineDescription = "Curries, biryani, naan, and aromatic spices",
                ImageUrl = "/images/cuisines/indian.png",
                IsActive = true
            },
            new Cuisine
            {
                CuisineId = 4,
                CuisineName = "Mexican",
                CuisineDescription = "Tacos, burritos, quesadillas, and traditional Mexican flavors",
                ImageUrl = "/images/cuisines/mexican.png",
                IsActive = true
            },
            new Cuisine
            {
                CuisineId = 5,
                CuisineName = "Japanese",
                CuisineDescription = "Sushi, ramen, tempura, and other Japanese favorites",
                ImageUrl = "/images/cuisines/japanese.png",
                IsActive = true
            },
            new Cuisine
            {
                CuisineId = 6,
                CuisineName = "American",
                CuisineDescription = "Burgers, BBQ, fried chicken, and comfort food",
                ImageUrl = "/images/cuisines/american.png",
                IsActive = true
            }
            );



        }
    }
}
