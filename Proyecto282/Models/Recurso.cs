using System;
using System.Collections.Generic;

namespace Proyecto282.Models;

public partial class Recurso
{
    public int IdRecurso { get; set; }

    public int? IdAmbiente { get; set; }

    public string? NombreRecurso { get; set; }

    public string? DescripcionRecurso { get; set; }

    public virtual Ambiente? IdAmbienteNavigation { get; set; }
}
