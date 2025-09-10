using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ModuloPages.Repositorio
{
    public interface IAuditarLogRepository
    {
        List<AuditarLog> GetByAuditarLogAsync(int? auditarPaginaId = null);
        Task<AuditarLog> AddAuditarLogAsync(AuditarLog AuditarLog);
        Task<AuditarLog> UpdateAuditarLogAsync(AuditarLog AuditarLog);
        Task<bool> DeleteAuditarLogAsync(int AuditarLogId);
    }
}
