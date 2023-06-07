using BackendWeatherAPP.Data;
using BackendWeatherAPP.Data.WeatherModels;
using Microsoft.EntityFrameworkCore;

namespace BackendWeatherAPP.Services
{
    public class UserIdService
    {
        private readonly WeatherDbContext _context;
        //Definir Constructor
        public UserIdService(WeatherDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario?> GetUserByUsername(string username)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(c => c.Username == username);
        }
    }
}
