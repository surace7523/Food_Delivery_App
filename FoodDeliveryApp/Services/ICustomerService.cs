using FoodDeliveryApp.ViewModels.Common;
using FoodDeliveryApp.ViewModels.Customer;
using FoodDeliveryApp.Models;
namespace FoodDeliveryApp.Services
{
    public interface ICustomerService
    {
        Task<bool> RegisterCustomerAsync(CustomerRegisterViewModel model);
        Task<User?> LoginCustomerAsync(LoginViewModel model);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordViewModel model);
        Task<bool> ForgotPasswordAsync(ForgotPasswordViewModel model);
        Task<bool> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<bool> AddAddressAsync(int userId, AddressViewModel model);
        Task<List<UserAddress>> GetAddressesAsync(int userId);
        Task<bool> UpdateAddressAsync(int userId, EditAddressViewModel model);
        Task<bool> DeleteAddressAsync(int addressId);
        Task<User?> GetCustomerByIdAsync(int userId);
        Task<bool> UpdateCustomerAccountAsync(AccountViewModel model);

    }
}
