using System;
using System.Collections.Generic;

namespace Proyecto282.Models;

public partial class Inscripcion
{
    public int IdInscripcion { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdEvento { get; set; }

    public string? Estatus { get; set; }

    public virtual Evento? IdEventoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
