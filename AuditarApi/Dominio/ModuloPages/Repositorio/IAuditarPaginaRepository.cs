using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ModuloPages.Repositorio
{
    public interface IAuditarPaginaRepository
    {
        List<AuditarPagina> GetByAuditarPaginaAsync(int? menuId = null, int? accionId = null);
        Task<AuditarPagina> AddAuditarPaginaAsync(AuditarPagina auditarPagina);
        Task<AuditarPagina> UpdateAuditarPaginaAsync(AuditarPagina auditarPagina);
        Task<bool> DeleteAuditarPaginaAsync(int auditarPaginaId);
    }
}