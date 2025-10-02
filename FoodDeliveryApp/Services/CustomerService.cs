using FoodDeliveryApp.Data;
using FoodDeliveryApp.Models;
using FoodDeliveryApp.ViewModels.Common;
using FoodDeliveryApp.ViewModels.Customers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace FoodDeliveryApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomerService> _logger;
        private readonly IEmailService _emailService;

        public CustomerService(ApplicationDbContext context, ILogger<CustomerService> logger, IEmailService emailService)
        {
            _context = context;
            _logger = logger;
            _emailService = emailService;
        }

        //Registers a new customer. checks for duplicate email and phone number before adding to the database.
        //hashes the password before storing it.saves the users and sends a confirmation email.
        public async Task<bool> RegisterCustomerAsync(CustomerRegisterViewModel model)
        {
             
            try
            {
                // 1. Check if user already exists by email or phone
                //var existingUser = await _context.Users
                //    .FirstOrDefaultAsync(u => u.Email == model.Email || u.PhoneNumber == model.PhoneNumber);

                //if (existingUser != null)
                //{
                //    _logger.LogWarning("Duplicate registration attempt for Email={Email}, Phone={Phone}", model.Email, model.PhoneNumber);
                //    return false;
                //}

                // 1. Hash the password
                string HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                //create a new user
                var customerUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PasswordHash = HashedPassword,
                    PhoneNumber = model.PhoneNumber,
                    RoleMasterId = (int)RoleMasterModel.Customer,

                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IsEmailVerified = true,
                    IsPhoneNumberVerified = true,
                    IsTwoFactorEnabled = false,
                    IsActive = true
                };

                _context.Users.Add(customerUser);
                await _context.SaveChangesAsync();

                //// 3. Save the new user
                //_context.Users.Add(newUser);
                //await _context.SaveChangesAsync();

                //// 4. Send welcome/confirmation email
                //await _emailService.SendWelcomeEmailAsync(newUser.Email, newUser.FirstName);

                //_logger.LogInformation("New customer registered successfully: {Email}", newUser.Email);
                return await _context;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering customer with Email={Email}", model.Email);
                throw;
            }
        }

        public Task<bool> AddAddressAsync(int userId, AddressViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePasswordAsync(int userId, ChangePasswordViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAddressAsync(int addressId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserAddress>> GetAddressesAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetCustomerByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> LoginCustomerAsync(LoginViewModel model)
        {
            throw new NotImplementedException();
        }



        public Task<bool> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAddressAsync(int userId, EditAddressViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCustomerAccountAsync(AccountViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
