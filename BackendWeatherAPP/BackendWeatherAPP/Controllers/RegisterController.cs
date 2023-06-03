using Microsoft.AspNetCore.Mvc;
using BackendWeatherAPP.Data;
using BackendWeatherAPP.Data.WeatherModels;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using BackendWeatherAPP.Services;


namespace BackendWeatherAPP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        //Crear servicio para Login
        private readonly RegisterService _accountService;
        //Definir Constructor
        public RegisterController(RegisterService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(Usuario user)
        {
            var existingUser = await _accountService.GetUserByName(user.Username);
            if (existingUser == null)
            {
                //La cuenta no existe => Crear cuenta
                await _accountService.RegisterUser(user);
                return Ok("Registro exitoso");
            }
            else
            {
                return BadRequest(new { message = $"El usuario {user.Username} ya existe." });
            }

        }
    }
}