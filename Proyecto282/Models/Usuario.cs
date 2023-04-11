using System;
using System.Collections.Generic;

namespace Proyecto282.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? TipoUsuario { get; set; }

    public string? Contrasena { get; set; }

    public virtual ICollection<Asistencia> Asistencia { get; } = new List<Asistencia>();

    public virtual ICollection<Certificado> Certificados { get; } = new List<Certificado>();

    public virtual ICollection<Comentario> ComentarioIdExpositorRespondeNavigations { get; } = new List<Comentario>();

    public virtual ICollection<Comentario> ComentarioIdUsuarioNavigations { get; } = new List<Comentario>();

    public virtual ICollection<HorarioExposicion> HorarioExposicions { get; } = new List<HorarioExposicion>();

    public virtual ICollection<Inscripcion> Inscripcions { get; } = new List<Inscripcion>();

    public virtual ICollection<Material> Materials { get; } = new List<Material>();

    public virtual ICollection<Reserva> Reservas { get; } = new List<Reserva>();
}
