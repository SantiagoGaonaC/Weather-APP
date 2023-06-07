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
