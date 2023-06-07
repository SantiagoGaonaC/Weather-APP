using BackendWeatherAPP.Data;
using BackendWeatherAPP.Data.WeatherModels;
using Microsoft.EntityFrameworkCore;

namespace BackendWeatherAPP.Services
{
    public class RegisterService
    {
        private readonly WeatherDbContext _context;
        //Definir Constructor
        public RegisterService(WeatherDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario?> GetUserById(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
        public async Task<Usuario?> GetUserByName(string name)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(c => c.Username == name);
        }
        //Se definen métodos en el servicio/clase que replican los métodos del controlador
        public async Task RegisterUser(Usuario user)
        {
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
        }
    }  
}