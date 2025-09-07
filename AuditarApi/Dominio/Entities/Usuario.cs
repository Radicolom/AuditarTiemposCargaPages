using System;
using System.Collections.Generic;

namespace Dominio.Entities;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ApellidoUsuario { get; set; } = null!;

    public string DocumentoUsuario { get; set; } = null!;

    public string CorreoUsuario { get; set; } = null!;

    public bool? EmailConfirmed { get; set; }

    public string PasswordUsuario { get; set; } = null!;

    public string? TelefonoUsuario { get; set; }

    public bool? TelefonoConfirmadoUsuario { get; set; }

    public bool? AutenticacionDobleFactor { get; set; }

    public int? AutenticacionIntentos { get; set; }

    public bool? EstadoUsuario { get; set; }

    public int? RolId { get; set; }

    public virtual ICollection<AuditarPagina> AuditarPaginas { get; set; } = new List<AuditarPagina>();

    public virtual Rol? Rol { get; set; }
}
