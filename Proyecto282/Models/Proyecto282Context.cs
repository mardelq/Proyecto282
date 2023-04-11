using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto282.Models;

public partial class Proyecto282Context : DbContext
{
    public Proyecto282Context()
    {
    }

    public Proyecto282Context(DbContextOptions<Proyecto282Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Ambiente> Ambientes { get; set; }

    public virtual DbSet<Asistencia> Asistencia { get; set; }

    public virtual DbSet<Certificado> Certificados { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<HorarioExposicion> HorarioExposicions { get; set; }

    public virtual DbSet<Infraestructura> Infraestructuras { get; set; }

    public virtual DbSet<Inscripcion> Inscripcions { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Recurso> Recursos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=RAZER;Initial Catalog=Proyecto282;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ambiente>(entity =>
        {
            entity.HasKey(e => e.IdAmbiente).HasName("PK__AMBIENTE__51688B22A0D8DF6D");

            entity.ToTable("AMBIENTE");

            entity.Property(e => e.IdAmbiente)
                .ValueGeneratedNever()
                .HasColumnName("id_ambiente");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.NombreAmbiente)
                .HasMaxLength(255)
                .HasColumnName("nombre_ambiente");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Ambientes)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("FK__AMBIENTE__id_eve__52593CB8");
        });

        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PK__ASISTENC__D0454A9AE9E39DB0");

            entity.ToTable("ASISTENCIA");

            entity.Property(e => e.IdAsistencia)
                .ValueGeneratedNever()
                .HasColumnName("id_asistencia");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("FK__ASISTENCI__id_ev__4BAC3F29");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__ASISTENCI__id_us__4AB81AF0");
        });

        modelBuilder.Entity<Certificado>(entity =>
        {
            entity.HasKey(e => e.IdCertificado).HasName("PK__CERTIFIC__B7A2D13B2C21023E");

            entity.ToTable("CERTIFICADO");

            entity.Property(e => e.IdCertificado)
                .ValueGeneratedNever()
                .HasColumnName("id_certificado");
            entity.Property(e => e.FechaEmision)
                .HasColumnType("date")
                .HasColumnName("fecha_emision");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.NombreEvento)
                .HasMaxLength(255)
                .HasColumnName("nombre_evento");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Certificados)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("FK__CERTIFICA__id_ev__4F7CD00D");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Certificados)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__CERTIFICA__id_us__4E88ABD4");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.IdComentario).HasName("PK__COMENTAR__1BA6C6F47F6D7304");

            entity.ToTable("COMENTARIO");

            entity.Property(e => e.IdComentario)
                .ValueGeneratedNever()
                .HasColumnName("id_comentario");
            entity.Property(e => e.Comentario1).HasColumnName("comentario");
            entity.Property(e => e.FechaComentario)
                .HasColumnType("datetime")
                .HasColumnName("fecha_comentario");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdExpositorResponde).HasColumnName("id_expositor_responde");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("FK__COMENTARI__id_ev__403A8C7D");

            entity.HasOne(d => d.IdExpositorRespondeNavigation).WithMany(p => p.ComentarioIdExpositorRespondeNavigations)
                .HasForeignKey(d => d.IdExpositorResponde)
                .HasConstraintName("FK__COMENTARI__id_ex__412EB0B6");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ComentarioIdUsuarioNavigations)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__COMENTARI__id_us__3F466844");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("PK__EVENTO__AF150CA5380D483C");

            entity.ToTable("EVENTO");

            entity.Property(e => e.IdEvento)
                .ValueGeneratedNever()
                .HasColumnName("id_evento");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.Lugar)
                .HasMaxLength(255)
                .HasColumnName("lugar");
            entity.Property(e => e.NombreEvento)
                .HasMaxLength(255)
                .HasColumnName("nombre_evento");
            entity.Property(e => e.Publico)
                .HasMaxLength(3)
                .HasColumnName("publico");
            entity.Property(e => e.TipoEvento)
                .HasMaxLength(50)
                .HasColumnName("tipo_evento");
        });

        modelBuilder.Entity<HorarioExposicion>(entity =>
        {
            entity.HasKey(e => e.IdHorarioExposicion).HasName("PK__HORARIO___80D74FCE9786E897");

            entity.ToTable("HORARIO_EXPOSICION");

            entity.Property(e => e.IdHorarioExposicion)
                .ValueGeneratedNever()
                .HasColumnName("id_horario_exposicion");
            entity.Property(e => e.FechaExposicion)
                .HasColumnType("date")
                .HasColumnName("fecha_exposicion");
            entity.Property(e => e.HoraFin).HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio).HasColumnName("hora_inicio");
            entity.Property(e => e.IdAmbiente).HasColumnName("id_ambiente");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdExpositor).HasColumnName("id_expositor");

            entity.HasOne(d => d.IdAmbienteNavigation).WithMany(p => p.HorarioExposicions)
                .HasForeignKey(d => d.IdAmbiente)
                .HasConstraintName("FK__HORARIO_E__id_am__5CD6CB2B");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.HorarioExposicions)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("FK__HORARIO_E__id_ev__5BE2A6F2");

            entity.HasOne(d => d.IdExpositorNavigation).WithMany(p => p.HorarioExposicions)
                .HasForeignKey(d => d.IdExpositor)
                .HasConstraintName("FK__HORARIO_E__id_ex__5DCAEF64");
        });

        modelBuilder.Entity<Infraestructura>(entity =>
        {
            entity.HasKey(e => e.IdInfraestructura).HasName("PK__INFRAEST__276F867D366D57DF");

            entity.ToTable("INFRAESTRUCTURA");

            entity.Property(e => e.IdInfraestructura)
                .ValueGeneratedNever()
                .HasColumnName("id_infraestructura");
            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Infraestructuras)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("FK__INFRAESTR__id_ev__47DBAE45");
        });

        modelBuilder.Entity<Inscripcion>(entity =>
        {
            entity.HasKey(e => e.IdInscripcion).HasName("PK__INSCRIPC__CB0117BA05FED0D8");

            entity.ToTable("INSCRIPCION");

            entity.Property(e => e.IdInscripcion)
                .ValueGeneratedNever()
                .HasColumnName("id_inscripcion");
            entity.Property(e => e.Estatus)
                .HasMaxLength(50)
                .HasColumnName("estatus");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("FK__INSCRIPCI__id_ev__3C69FB99");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__INSCRIPCI__id_us__3B75D760");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.IdMaterial).HasName("PK__MATERIAL__81E99B834293CD57");

            entity.ToTable("MATERIAL");

            entity.Property(e => e.IdMaterial)
                .ValueGeneratedNever()
                .HasColumnName("id_material");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Disponible)
                .HasMaxLength(3)
                .HasColumnName("disponible");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdExpositorSube).HasColumnName("id_expositor_sube");
            entity.Property(e => e.NombreMaterial)
                .HasMaxLength(255)
                .HasColumnName("nombre_material");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.IdEvento)
                .HasConstraintName("FK__MATERIAL__id_eve__440B1D61");

            entity.HasOne(d => d.IdExpositorSubeNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.IdExpositorSube)
                .HasConstraintName("FK__MATERIAL__id_exp__44FF419A");
        });

        modelBuilder.Entity<Recurso>(entity =>
        {
            entity.HasKey(e => e.IdRecurso).HasName("PK__RECURSO__2B386DE4C2906EFC");

            entity.ToTable("RECURSO");

            entity.Property(e => e.IdRecurso)
                .ValueGeneratedNever()
                .HasColumnName("id_recurso");
            entity.Property(e => e.DescripcionRecurso).HasColumnName("descripcion_recurso");
            entity.Property(e => e.IdAmbiente).HasColumnName("id_ambiente");
            entity.Property(e => e.NombreRecurso)
                .HasMaxLength(255)
                .HasColumnName("nombre_recurso");

            entity.HasOne(d => d.IdAmbienteNavigation).WithMany(p => p.Recursos)
                .HasForeignKey(d => d.IdAmbiente)
                .HasConstraintName("FK__RECURSO__id_ambi__5535A963");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__RESERVA__423CBE5DB8B4D6B4");

            entity.ToTable("RESERVA");

            entity.Property(e => e.IdReserva)
                .ValueGeneratedNever()
                .HasColumnName("id_reserva");
            entity.Property(e => e.FechaReserva)
                .HasColumnType("date")
                .HasColumnName("fecha_reserva");
            entity.Property(e => e.HoraFin).HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio).HasColumnName("hora_inicio");
            entity.Property(e => e.IdAmbiente).HasColumnName("id_ambiente");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdAmbienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdAmbiente)
                .HasConstraintName("FK__RESERVA__id_ambi__59063A47");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__RESERVA__id_usua__5812160E");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__4E3E04AD210EC1C1");

            entity.ToTable("USUARIO");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("id_usuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .HasColumnName("contrasena");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(255)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(50)
                .HasColumnName("tipo_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
