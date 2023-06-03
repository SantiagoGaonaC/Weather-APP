using BackendWeatherAPP.Data;
using BackendWeatherAPP.Data.DTOs;
using BackendWeatherAPP.Data.WeatherModels;
using Microsoft.EntityFrameworkCore;

namespace BackendWeatherAPP.Services
{
    public class LoginService
    {
        private readonly WeatherDbContext _context;
        //Definir Constructor
        public LoginService(WeatherDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario?> GetUser(UsuarioDTO usuario)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(c => c.Username == usuario.Username && c.Password == usuario.Password);
        }
    }
}
