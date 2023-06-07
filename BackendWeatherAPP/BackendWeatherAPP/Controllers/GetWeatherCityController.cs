using Microsoft.AspNetCore.Mvc;
using BackendWeatherAPP.Services;
using Microsoft.AspNetCore.Authorization;

namespace BackendWeatherAPP.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GetWeatherCityController : ControllerBase
    {
        private readonly GetWeatherCityService _getWeatherCityService;
        //Definir Constructor
        public GetWeatherCityController(GetWeatherCityService getWeatherCityService)
        {
            _getWeatherCityService = getWeatherCityService;
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetWeatherCity(string name)
        {
            var response = await _getWeatherCityService.GetWeatherAPI(name);

            if (response.IsSuccessStatusCode)
            {
                return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType.MediaType);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }


    }
}