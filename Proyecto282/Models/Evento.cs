using System;
using System.Collections.Generic;

namespace Proyecto282.Models;

public partial class Evento
{
    public int IdEvento { get; set; }

    public string? NombreEvento { get; set; }

    public DateTime? Fecha { get; set; }

    public TimeSpan? Hora { get; set; }

    public string? Lugar { get; set; }

    public string? TipoEvento { get; set; }

    public string? Publico { get; set; }

    public virtual ICollection<Ambiente> Ambientes { get; } = new List<Ambiente>();

    public virtual ICollection<Asistencia> Asistencia { get; } = new List<Asistencia>();

    public virtual ICollection<Certificado> Certificados { get; } = new List<Certificado>();

    public virtual ICollection<Comentario> Comentarios { get; } = new List<Comentario>();

    public virtual ICollection<HorarioExposicion> HorarioExposicions { get; } = new List<HorarioExposicion>();

    public virtual ICollection<Infraestructura> Infraestructuras { get; } = new List<Infraestructura>();

    public virtual ICollection<Inscripcion> Inscripcions { get; } = new List<Inscripcion>();

    public virtual ICollection<Material> Materials { get; } = new List<Material>();
}
