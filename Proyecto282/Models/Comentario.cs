using System;
using System.Collections.Generic;

namespace Proyecto282.Models;

public partial class Comentario
{
    public int IdComentario { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdEvento { get; set; }

    public string? Comentario1 { get; set; }

    public int? IdExpositorResponde { get; set; }

    public DateTime? FechaComentario { get; set; }

    public virtual Evento? IdEventoNavigation { get; set; }

    public virtual Usuario? IdExpositorRespondeNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
