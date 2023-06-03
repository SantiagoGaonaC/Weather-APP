using BackendWeatherAPP.Data;
using BackendWeatherAPP.Data.WeatherModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

//Service Layer
namespace BackendWeatherAPP.Services
{
    public class WeatherService
    {
        private readonly WeatherDbContext _context;
        //Definir Constructor
        public WeatherService(WeatherDbContext context)
        {
            _context = context;
        }

        //Se definen métodos en el servicio/clase que replican los métodos del controlador

        public async Task<Ciudade?> GetCityById(int id)
        {
            return await _context.Ciudades.FindAsync(id);
        }
        public async Task<Ciudade?> GetCityByName(string name)
        {
            return await _context.Ciudades.FirstOrDefaultAsync(c => c.NombreCiudad == name);
        }

        public async Task<List<Ciudade?>> GetCityData(string cityName)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var keyApi = config.GetSection("KeyAPI").Value;
            string apiUrl = $"{keyApi}{cityName}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var cityData = JsonSerializer.Deserialize<List<Ciudade>>(jsonResponse, options);

                    return cityData;
                }
                else
                {
                    // Manejo del error de la API
                    throw new Exception("Error al obtener datos de la API.");
                }
            }
        }
        public async Task SaveCity(Ciudade city)
        {
            _context.Ciudades.Add(city);
            await _context.SaveChangesAsync();
        }
    }
}