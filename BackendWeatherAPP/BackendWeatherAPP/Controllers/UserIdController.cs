using Microsoft.AspNetCore.Mvc;
using BackendWeatherAPP.Services;
using Microsoft.AspNetCore.Authorization;

namespace BackendWeatherAPP.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserIdController : ControllerBase
    {
        private readonly UserIdService _userIdService;
        public UserIdController(UserIdService userIdService)
        {
            _userIdService = userIdService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserId()
        {
            var username = User.Identity.Name;
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(new { message = "Usuario no encontrado" });
            }

            var user = await _userIdService.GetUserByUsername(username);
            if (user == null)
            {
                return BadRequest(new { message = "Usuario no encontrado" });
            }

            return Ok(new { userId = user.Id, username = user.Username });
        }
    }
}