using Microsoft.AspNetCore.Mvc;
using BackendWeatherAPP.Data;
using BackendWeatherAPP.Data.WeatherModels;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using BackendWeatherAPP.Services;
using Microsoft.AspNetCore.Authorization;

namespace BackendWeatherAPP.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        //Definir Constructor
        public CityController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ciudade?>> GetById(int id)
        {
            var ciudad = await _weatherService.GetCityById(id);
            if (ciudad == null)
            {
                return NotFound(new { message = $"La ciudad con ID = {id} no existe."});
            }
            return Ok(ciudad);
        }

        [HttpPost("createcity")]
        public async Task<IActionResult> CreateCity(Ciudade city)
        {
            var existingCity = await _weatherService.GetCityByName(city.NombreCiudad);

            if (existingCity != null)
            {
                // La ciudad ya existe en la base de datos, devolver los datos existentes
                return CreatedAtAction(nameof(GetById), new { id = existingCity.Id }, existingCity);
            }
            else
            {
                // La ciudad no existe en la base de datos, obtener datos de la API GEO
                var cityData = await _weatherService.GetCityData(city.NombreCiudad);

                if (cityData != null && cityData.Count > 0)
                {
                    var firstCity = cityData.FirstOrDefault();
                    // Datos de la ciudad obtenidos de la API, guardar en la base de datos
                    city.Lon = firstCity.Lon;
                    city.Lat = firstCity.Lat;
                    await _weatherService.SaveCity(city);

                    return CreatedAtAction(nameof(GetById), new { id = city.Id }, city);
                }
                else
                {
                    // No se encontraron datos de la ciudad en la API
                    return NotFound("La ciudad no se encuentra o no existe en la API.");
                }
            }
        }
    }
}