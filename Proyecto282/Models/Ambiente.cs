using System;
using System.Collections.Generic;

namespace Proyecto282.Models;

public partial class Ambiente
{
    public int IdAmbiente { get; set; }

    public int? IdEvento { get; set; }

    public string? NombreAmbiente { get; set; }

    public virtual ICollection<HorarioExposicion> HorarioExposicions { get; } = new List<HorarioExposicion>();

    public virtual Evento? IdEventoNavigation { get; set; }

    public virtual ICollection<Recurso> Recursos { get; } = new List<Recurso>();

    public virtual ICollection<Reserva> Reservas { get; } = new List<Reserva>();
}
