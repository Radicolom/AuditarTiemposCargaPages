using System;
using System.Collections.Generic;

namespace Dominio.Entities;

public partial class AuditarPagina
{
    public int AuditarPaginaId { get; set; }

    public string UrlAuditarPagina { get; set; } = null!;

    public string NombreAuditarPagina { get; set; } = null!;

    public bool? EstadoAuditarPagina { get; set; }

    public DateTime? FechaCreacionAuditarPagina { get; set; }

    public int? UsuarioId { get; set; }

    public virtual ICollection<AuditarLog> AuditarLogs { get; set; } = new List<AuditarLog>();

    public virtual Usuario? Usuario { get; set; }
}
