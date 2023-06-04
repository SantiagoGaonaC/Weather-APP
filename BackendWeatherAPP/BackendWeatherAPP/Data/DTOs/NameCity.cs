using System.ComponentModel.DataAnnotations;

namespace BackendWeatherAPP.Data.DTOs
{
    public class NameCity
    {
        [Required(ErrorMessage = "El nombre de la Ciudad es requerida.")]
        [MaxLength(90, ErrorMessage = "El Nombre de la Ciudad debe ser menor a 90 caracteres.")]
        public string NombreCiudad { get; set; } = null!;
    }
}
