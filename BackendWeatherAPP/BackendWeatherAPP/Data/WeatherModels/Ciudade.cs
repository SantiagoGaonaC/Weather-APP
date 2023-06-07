using System.ComponentModel.DataAnnotations;

namespace BackendWeatherAPP.Data.WeatherModels
{
    public class Ciudade
    {
        public int Id { get; set; }

        [MaxLength(90,ErrorMessage = "El Nombre de la Ciudad debe ser menor a 90 caracteres.")]
        public string NombreCiudad { get; set; } = null!;

        public double Lon { get; set; }

        public double Lat { get; set; }

        public virtual ICollection<Usuario> IdUsuarios { get; set; } = new List<Usuario>();
    }
}

