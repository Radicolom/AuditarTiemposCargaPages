using System;
using System.Collections.Generic;

namespace Dominio.Entities;

public partial class RolOperacionAccion
{
    public int RolOperacionAccionId { get; set; }

    public int? RolId { get; set; }

    public int? ServicioId { get; set; }

    public int? AccionId { get; set; }

    public virtual Accion? Accion { get; set; }

    public virtual Rol? Rol { get; set; }

    public virtual Servicio? Servicio { get; set; }
}
