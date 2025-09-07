using System;
using System.Collections.Generic;

namespace Dominio.Entities;

public partial class Servicio
{
    public int ServicioId { get; set; }

    public string NombreServicio { get; set; } = null!;

    public virtual ICollection<RolOperacionAccion> RolOperacionAccions { get; set; } = new List<RolOperacionAccion>();
}
