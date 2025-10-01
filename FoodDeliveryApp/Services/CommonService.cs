using FoodDeliveryApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Services
{
    public class CommonService : ICommonService
    {
        private readonly ApplicationDbContext _context;
        public CommonService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsEmailDuplicateAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<bool> IsPhoneDuplicateAsync(string phone)
        {
            return await _context.Users.AnyAsync(u => u.PhoneNumber == phone);
        }
    }
}

