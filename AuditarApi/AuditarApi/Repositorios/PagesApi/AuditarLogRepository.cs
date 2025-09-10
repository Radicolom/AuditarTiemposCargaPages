using Dominio.Entities;
using Dominio.ModuloPages.Repositorio;
using Dominio.Data;
using Microsoft.EntityFrameworkCore;

public class AuditarLogRepository : IAuditarLogRepository
{
    private readonly ApplicationDbContext _context;

    public AuditarLogRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<AuditarLog> GetByAuditarLogAsync(int? auditarLogId = null)
    {
        var query = _context.AuditarLogs
            .Include(x => x.AuditarPagina)
            .AsQueryable();

        if (auditarLogId.HasValue)
            query = query.Where(x => x.AuditarLogId == auditarLogId.Value);

        return query.ToList();
    }

    public async Task<AuditarLog> AddAuditarLogAsync(AuditarLog auditarLog)
    {
        _context.AuditarLogs.Add(auditarLog);
        await _context.SaveChangesAsync();
        return auditarLog;
    }

    public async Task<AuditarLog> UpdateAuditarLogAsync(AuditarLog auditarLog)
    {
        _context.AuditarLogs.Update(auditarLog);
        await _context.SaveChangesAsync();
        return auditarLog;
    }

    public async Task<bool> DeleteAuditarLogAsync(int auditarLogId)
    {
        var entity = await _context.AuditarLogs.FindAsync(auditarLogId);
        if (entity == null)
            return false;

        _context.AuditarLogs.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}