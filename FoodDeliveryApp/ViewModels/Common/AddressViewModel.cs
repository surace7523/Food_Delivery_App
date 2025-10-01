using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApp.ViewModels.Common
{
    public class AddressViewModel
    {
    
        [StringLength(50, ErrorMessage = "Label cannot exceed 50 characters")]
        public string? Label { get; set; } // e.g., Home, Office

        [Required(ErrorMessage = "Address Line is required")]
        [StringLength(200, ErrorMessage = "Address Line cannot exceed 200 characters")]
        public string AddressLine { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(100, ErrorMessage = "State cannot exceed 100 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$",
            ErrorMessage = "Invalid Zip Code format (use 12345 or 12345-6789)")]
        public string ZipCode { get; set; }

        // Optional geolocation fields
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }

}
