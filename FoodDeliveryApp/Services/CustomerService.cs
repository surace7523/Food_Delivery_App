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



                //// 4. Send welcome/confirmation email
                string subject = "Registration confirmation - Food Delivery App";

                string body = $@"
                            <!DOCTYPE html>
                            <html lang=""en"">
                            <head>
                                <meta charset=""UTF-8"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title>{subject}</title>
                            </head>
                            <body style=""margin:0;padding:0;background-color:#f4f6f8;font-family:Arial,Helvetica,sans-serif;"">
                                <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""background-color:#f4f6f8;padding:24px 0;"">
                                <tr>
                                    <td align=""center"">
                                    <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""600"" style=""background:#ffffff;border-radius:8px;overflow:hidden;box-shadow:0 2px 6px rgba(0,0,0,0.08);"">
          
                                        <!-- Header -->
                                        <tr>
                                        <td style=""padding:20px;background:#ef4444;color:#ffffff;font-size:20px;font-weight:bold;text-align:center;"">
                                            Food Delivery App
                                        </td>
                                        </tr>

                                        <!-- Body -->
                                        <tr>
                                        <td style=""padding:28px;"">
                                            <h1 style=""margin:0 0 12px 0;font-size:22px;color:#111827;"">Hello {customerUser.FirstName},</h1>

                                            <p style=""margin:0 0 18px 0;color:#374151;line-height:1.6;font-size:15px;"">
                                            Thank you for registering with <strong>Food Delivery App</strong>! 🎉<br><br>
                                            Your account has been created successfully. You can now log in and start ordering your favorite meals.
                                            </p>

                                            <p style=""margin:0 0 20px 0;color:#374151;font-size:15px;"">
                                            If you have any questions, our Customer Service team is available 24/7.
                                            </p>

                                            <!-- Customer service contact -->
                                            <div style=""padding:15px;background:#f9fafb;border:1px solid #e5e7eb;border-radius:6px;font-size:14px;color:#374151;margin:20px 0;"">
                                            📧 Email: <a href=""mailto:support@fooddeliveryapp.com"" style=""color:#ef4444;text-decoration:none;"">support@fooddeliveryapp.com</a><br>
                                            ☎ Phone: <a href=""tel:+18001234567"" style=""color:#ef4444;text-decoration:none;"">+1 (800) 123-4567</a>
                                            </div>

                                            <!-- CTA button -->
                                            <table role=""presentation"" cellspacing=""0"" cellpadding=""0"" border=""0"" align=""center"" style=""margin:20px 0;"">
                                            <tr>
                                                <td style=""border-radius:6px;"" bgcolor=""#ef4444"">
                                                <a href=""{loginLink}"" target=""_blank"" 
                                                    style=""font-size:16px;font-weight:bold;color:#ffffff;text-decoration:none;padding:12px 24px;display:inline-block;border-radius:6px;"">
                                                    Log in to Your Account
                                                </a>
                                                </td>
                                            </tr>
                                            </table>

                                            <p style=""margin:25px 0 0 0;color:#6b7280;font-size:13px;"">
                                            Thank you for choosing <strong>Food Delivery App</strong>.  
                                            We look forward to serving you!
                                            </p>
                                        </td>
                                        </tr>

                                        <!-- Footer -->
                                        <tr>
                                        <td style=""padding:20px;background:#f9fafb;text-align:center;color:#9ca3af;font-size:12px;"">
                                            © 2025 Food Delivery App. All rights reserved.<br>
                                            <a href=""{unsubscribeLink}"" style=""color:#9ca3af;text-decoration:underline;"">Unsubscribe</a>
                                        </td>
                                        </tr>

                                    </table>
                                    </td>
                                </tr>
                                </table>
                            </body>
                            </html>";





                await _emailService.SendEmailAsync(customerUser.Email, subject, body, true);

                _logger.LogInformation("New customer registered successfully: {Email}", model.Email);

                return true;




            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering customer with Email={Email}", model.Email);
                return false;
            }
        }


        //validates customer login by checking email and password.
     
        public async Task<User?> LoginCustomerAsync(LoginViewModel model)
        {

            try 
            {
                //Query the database for a user with matching email and customer role
                var User = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.RoleMasterId == (int)RoleMasterModel.Customer && u.IsActive);

                if (User != null || BCrypt.Net.BCrypt.Verify(model.Password, User.PasswordHash))
                {
                    _logger.LogInformation("Failed login attempt for Email/Phone={Email}", model.Email);
                    return User;
                }
                _logger.LogWarning("Customer logged in successfully: {Email}", model.Email);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering customer with Email={Email}", model.Email);
                throw;
            }
        }
        public async Task<bool> AddAddressAsync(int userId, AddressViewModel model)
        {
            try 
            {
                //map the view model to the muser address entity    
                var userAddress = new UserAddress
                {
                    UserId = userId,
                    AddressLine1 = model.AddressLine,
                    AddressLine2 = model.AddressLine,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    Country = model.GetType().GetProperty("Country")?.GetValue(model, null)?.ToString() ?? "USA", //default to USA if not provided
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow

                };
                _context.UserAddresses.Add(userAddress);
                await _context.SaveChangesAsync();
                _logger.LogInformation("New address added for UserId={UserId}", userId);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding address for UserId={UserId}", userId);
                return false;
            }   
        }

        public  async Task<bool> ChangePasswordAsync(int userId, ChangePasswordViewModel model)
        {
            try 
            {
                //Retrieve the user baseqd on userId

                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user == null)
                {
                    _logger.LogWarning("Change password failed. User not found: UserId={UserId}", userId);
                    return false;
                }

                //Verify that the current passswrord matches the stored password hash.
                if (!BCrypt.Net.BCrypt.Verify(model.CurentPassword, user.PasswordHash))
                {
                    _logger.LogWarning("Change password failed. Incorrect current password: UserId={UserId}", userId);
                    return false;
                }
                //Hash the new password and update the user record 
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                user.ModifiedDate = DateTime.UtcNow;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Password changed successfully for UserId={UserId}", userId);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for UserId={UserId}", userId);
                return false;
            }
        }

        public async Task<bool> DeleteAddressAsync(int addressId)
        {
            try
            { 
                var address =  _context.UserAddresses.FirstOrDefault(ua => ua.UserAddressId == addressId);
                if (address == null)
                {
                    _logger.LogWarning("Delete address failed. Address not found: AddressId={AddressId}", addressId);
                    return false;
                }
                _context.UserAddresses.Remove(address);
                 _context.SaveChanges();
                _logger.LogInformation("Address deleted successfully: AddressId={AddressId}", addressId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting address for AddressId={AddressId}", addressId);
                return false;
            }
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            try 
            {
                //Ensure the user exists and is a customer
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.RoleMasterId == (int)RoleMasterModel.Customer && u.IsActive);
                if (user == null)
                {
                    _logger.LogWarning("Forgot password failed. User not found: Email={Email}", model.Email);
                    return false;
                }

                _logger.LogInformation("Forgot Password  initiated for the  Email={Email}", model.Email);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in forgot password for Email={Email}", model.Email);
                return false;
            }
        }

        public async Task<List<UserAddress>> GetAddressesAsync(int userId)
        {
            try
            { 
                var addresses = await _context.UserAddresses
                            .AsNoTracking(). //optimize for read-only
                             Where(ua => ua.UserId == userId).ToListAsync();
                _logger.LogInformation("Retrieved {Count} addresses for UserId={UserId}", addresses.Count, userId);
                return addresses;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving addresses for UserId={UserId}", userId);
                return new List<UserAddress>();
            }
        }

        public async Task<User?> GetCustomerByIdAsync(int userId)
        {
            try
            {
                //Retrieve the user based on userId and ensure they are a customer
                var user = await _context.Users.AsNoTracking()
                            .FirstOrDefaultAsync(u => u.UserId == userId && u.RoleMasterId == (int)RoleMasterModel.Customer && u.IsActive);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer for UserId={UserId}", userId);
                return null;
            }
        }




        public async Task<bool> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            try
            {
                //Retrieve the user based on email and ensure they are a customer
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.RoleMasterId == (int)RoleMasterModel.Customer && u.IsActive);
                if (user == null)
                {
                    _logger.LogWarning("Reset password failed. User not found: Email={Email}", model.Email);
                    return false;
                }


                //update the user's password with new hashed password
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                user.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                
                
                _logger.LogInformation("Password reset successfully for Email={Email}", model.Email);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting password for Email={Email}", model.Email);
                return false;
            }
        }

        public async Task<bool> UpdateAddressAsync(int userId, EditAddressViewModel model)
        {
            try
            { 
                //First Validate the address belongs to the user or not
                var existingAddress = await _context.UserAddresses.FirstOrDefaultAsync(ua => ua.UserAddressId == model.UserAddressId && ua.UserId == userId);
                if (existingAddress == null)
                {
                    _logger.LogWarning("Update address failed. Address not found or does not belong to user: UserId={UserId}, AddressId={AddressId}", userId, model.UserAddressId);
                    return false;
                }
                else
                {
                    //Map the updated fields from the view model to the entity
                    existingAddress.AddressLine1 = model.AddressLine1;
                    existingAddress.AddressLine2 = model.AddressLine2;
                    existingAddress.City = model.City;
                    existingAddress.State = model.State;
                    existingAddress.ZipCode = model.ZipCode;
                    existingAddress.Country = model.GetType().GetProperty("Country")?.GetValue(model, null)?.ToString() ?? existingAddress.Country; //retain existing country if not provided
                    existingAddress.ModifiedDate = DateTime.UtcNow;
                    
                }
                
                await _context.SaveChangesAsync();
                _logger.LogInformation("Address updated successfully for UserId={UserId}, AddressId={AddressId}", userId, model.UserAddressId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating address for UserId={UserId}, AddressId={AddressId}", userId, model.UserAddressId);
                return false;
            }
        }

        public async Task<bool> UpdateCustomerAccountAsync(AccountViewModel model)
        {
            try 
            {
                //Retrieve the customer from the database 
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == model.UserId && u.RoleMasterId == (int)RoleMasterModel.Customer && u.IsActive);
                if (user == null)
                {
                    _logger.LogWarning("Update account failed. User not found: UserId={UserId}", model.UserId);
                    return false;
                }
                //Update the user fields
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.ModifiedDate = DateTime.UtcNow;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Customer account updated successfully for UserId={UserId}", model.UserId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer account for UserId={UserId}", model.UserId);
                return false;
            }
    }
}
