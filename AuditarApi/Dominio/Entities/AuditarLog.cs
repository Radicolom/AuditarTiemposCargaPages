using System;
using System.Collections.Generic;

namespace Dominio.Entities;

public partial class AuditarLog
{
    public int AuditarLogId { get; set; }

    public int AuditarPaginaId { get; set; }

    public DateTime? FechaCreacionAuditarLog { get; set; }

    public int PageLoadTime { get; set; }

    public int ServerConnectionTime { get; set; }

    public int TimeToFirstByte { get; set; }

    public int ContentDownloadTime { get; set; }

    public int DomProcessingTime { get; set; }

    public bool EstadoAuditarPagina { get; set; }

    public virtual AuditarPagina AuditarPagina { get; set; } = null!;
}
