using System;
using System.Collections.Generic;

namespace Dominio.Entities;

public partial class Rol
{
    public int RolId { get; set; }

    public string NombreRol { get; set; } = null!;

    public bool? EstadoRol { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; set; } = new List<MenuRol>();

    public virtual ICollection<RolOperacionAccion> RolOperacionAccions { get; set; } = new List<RolOperacionAccion>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
