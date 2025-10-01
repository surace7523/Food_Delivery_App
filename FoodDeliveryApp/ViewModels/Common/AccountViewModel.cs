using System.ComponentModel.DataAnnotations;
namespace FoodDeliveryApp.ViewModels.Common
{
    public class AccountViewModel
    {

        public int UserId { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        //[DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$",
        //    ErrorMessage = "Password must be at least 8 characters, contain uppercase, lowercase, number, and special character.")]
        //public string Password { get; set; }

        //[Required(ErrorMessage = "Confirm Password is required.")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Passwords do not match.")]
        //public string ConfirmPassword { get; set; }

        public string? PhoneNumber { get; set; }

 
    }
}
