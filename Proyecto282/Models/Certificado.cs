using System;
using System.Collections.Generic;

namespace Proyecto282.Models;

public partial class Certificado
{
    public int IdCertificado { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdEvento { get; set; }

    public DateTime? FechaEmision { get; set; }

    public string? NombreEvento { get; set; }

    public virtual Evento? IdEventoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
