using System;
using System.Collections.Generic;

namespace Dominio.Entities;

public partial class Menu
{
    public int MenuId { get; set; }

    public string NombreMenu { get; set; } = null!;

    public string? UrlMenu { get; set; }

    public string? IconoMenu { get; set; }

    public bool? EstadoMenu { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; set; } = new List<MenuRol>();
}
