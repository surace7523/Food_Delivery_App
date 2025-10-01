namespace FoodDeliveryApp.Services
{
    public interface ICommonService
    {
        Task<bool>IsEmailDuplicateAsync(string email);
        Task<bool>IsPhoneDuplicateAsync(string phone);
    }
}
