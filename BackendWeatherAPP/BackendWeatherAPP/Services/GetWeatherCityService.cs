using BackendWeatherAPP.Data;
using BackendWeatherAPP.Data.WeatherModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using BackendWeatherAPP.Data.DTOs;

namespace BackendWeatherAPP.Services
{
    public class GetWeatherCityService
    {
        public async Task<HttpResponseMessage> GetWeatherAPI(string nameCity)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var getWeather = config.GetSection("GetWeather").Value;
            string apiUrl = $"{getWeather}{nameCity}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                return response;
            }
        }

    }
}
