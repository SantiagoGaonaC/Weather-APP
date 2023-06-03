using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendWeatherAPP.Data.WeatherModels;

public partial class Usuario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El Nombre de Usuario es requerido.")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "El Password es requerido.")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y maximo {1} caracteres de longitud.", MinimumLength = 6)]
    public string Password { get; set; } = null!;

    public virtual ICollection<Sesione> Sesiones { get; set; } = new List<Sesione>();

    public virtual ICollection<Ciudade> IdCiudads { get; set; } = new List<Ciudade>();
}
