using System;
using System.Collections.Generic;

namespace Proyecto282.Models;

public partial class Material
{
    public int IdMaterial { get; set; }

    public int? IdEvento { get; set; }

    public string? NombreMaterial { get; set; }

    public string? Descripcion { get; set; }

    public string? Disponible { get; set; }

    public int? IdExpositorSube { get; set; }

    public virtual Evento? IdEventoNavigation { get; set; }

    public virtual Usuario? IdExpositorSubeNavigation { get; set; }
}
