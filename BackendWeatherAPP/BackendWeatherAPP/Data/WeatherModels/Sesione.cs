using System;
using System.Collections.Generic;

namespace BackendWeatherAPP.Data.WeatherModels;

public partial class Sesione
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public DateTime FechaInicio { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
