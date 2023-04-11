using System;
using System.Collections.Generic;

namespace Proyecto282.Models;

public partial class HorarioExposicion
{
    public int IdHorarioExposicion { get; set; }

    public int? IdEvento { get; set; }

    public int? IdAmbiente { get; set; }

    public int? IdExpositor { get; set; }

    public DateTime? FechaExposicion { get; set; }

    public TimeSpan? HoraInicio { get; set; }

    public TimeSpan? HoraFin { get; set; }

    public virtual Ambiente? IdAmbienteNavigation { get; set; }

    public virtual Evento? IdEventoNavigation { get; set; }

    public virtual Usuario? IdExpositorNavigation { get; set; }
}
