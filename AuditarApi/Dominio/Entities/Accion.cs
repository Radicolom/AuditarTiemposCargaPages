using System;
using System.Collections.Generic;

namespace Dominio.Entities;

public partial class Accion
{
    public int AccionId { get; set; }

    public string NombreAccion { get; set; } = null!;

    public virtual ICollection<RolOperacionAccion> RolOperacionAccions { get; set; } = new List<RolOperacionAccion>();
}
