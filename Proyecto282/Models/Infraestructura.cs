using System;
using System.Collections.Generic;

namespace Proyecto282.Models;

public partial class Infraestructura
{
    public int IdInfraestructura { get; set; }

    public int? IdEvento { get; set; }

    public string? Nombre { get; set; }

    public string? Tipo { get; set; }

    public int? Capacidad { get; set; }

    public virtual Evento? IdEventoNavigation { get; set; }
}
