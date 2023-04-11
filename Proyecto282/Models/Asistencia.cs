using System;
using System.Collections.Generic;

namespace Proyecto282.Models;

public partial class Asistencia
{
    public int IdAsistencia { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdEvento { get; set; }

    public DateTime? Fecha { get; set; }

    public TimeSpan? Hora { get; set; }

    public virtual Evento? IdEventoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
