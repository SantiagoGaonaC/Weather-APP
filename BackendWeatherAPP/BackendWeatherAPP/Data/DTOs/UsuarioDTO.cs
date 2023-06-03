using System.ComponentModel.DataAnnotations;

namespace BackendWeatherAPP.Data.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre de Usuario es requerido.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "El Password es requerido.")]
        public string Password { get; set; } = null!;
    }
}