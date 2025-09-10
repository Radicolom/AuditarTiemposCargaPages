using System;
using System.Collections.Generic;

namespace Dominio.Entities;

public partial class AuditarLog
{
    public int AuditarLogId { get; set; }

    public int AuditarPaginaId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public bool EstadoAuditarPagina { get; set; }

    public int? PerformanceScore { get; set; }

    public int? TimeToFirstByteMs { get; set; }

    public int? DomProcessingTimeMs { get; set; }

    public int? PageLoadTimeMs { get; set; }

    public string? FcpValue { get; set; }

    public string? LcpValue { get; set; }

    public string? ClsValue { get; set; }

    public string? SpeedIndexValue { get; set; }

    public virtual AuditarPagina AuditarPagina { get; set; } = null!;
}
