using System;
using System.Collections.Generic;

namespace Proyecto282.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdAmbiente { get; set; }

    public DateTime? FechaReserva { get; set; }

    public TimeSpan? HoraInicio { get; set; }

    public TimeSpan? HoraFin { get; set; }

    public virtual Ambiente? IdAmbienteNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
