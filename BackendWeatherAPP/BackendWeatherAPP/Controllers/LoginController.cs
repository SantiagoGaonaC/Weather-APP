using Microsoft.AspNetCore.Mvc;
using BackendWeatherAPP.Data;
using BackendWeatherAPP.Data.WeatherModels;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using BackendWeatherAPP.Services;
using BackendWeatherAPP.Data.DTOs;
using BackendWeatherAPP.Data.WeatherModels;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BackendWeatherAPP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _accountService;
        private IConfiguration _config;
        //Definir Constructor
        public LoginController(LoginService accountService, IConfiguration config)
        {
            this._accountService = accountService;
            this._config = config;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(UsuarioDTO usuario)
        {
            var user = await _accountService.GetUser(usuario);
            if(user == null)
            {
                return BadRequest(new { message = "Usuario o contraseña incorrectos" });
            }
                
            string jwtToken = GenerateToken(user);
            return Ok(new { token = jwtToken });
        }

        private string GenerateToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
