namespace FoodDeliveryApp.Services
{
    public class EmailService : IEmailService
    {
        public Task SendWelcomeEmailAsync(string email, string firstName)
        {
            // Implementation e.g. SMTP, SendGrid, etc.
            return Task.CompletedTask;
        }
    }
}
