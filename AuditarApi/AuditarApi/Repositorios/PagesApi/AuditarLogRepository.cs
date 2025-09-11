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

	public List<AuditarLog> GetByAuditarLogAsync(AuditarLog auditarLog)
	{
		var query = _context.AuditarLogs
			.Include(x => x.AuditarPagina)
			.AsQueryable();

        // Filtros dinámicos basados en las propiedades no nulas del objeto AuditarLog
		if (auditarLog.EstadoAuditarPagina)
			query = query.Where(x => x.EstadoAuditarPagina == auditarLog.EstadoAuditarPagina && x.PageLoadTimeMs != null && x.PerformanceScore != null && x.TimeToFirstByteMs != null &&
									x.DomProcessingTimeMs != null && x.FcpValue != null && x.LcpValue != null && x.ClsValue != null && x.SpeedIndexValue != null );

        if (auditarLog.AuditarLogId > 0)
			query = query.Where(x => x.AuditarLogId == auditarLog.AuditarLogId);

		if (auditarLog.AuditarPaginaId > 0)
			query = query.Where(x => x.AuditarPaginaId == auditarLog.AuditarPaginaId);

		if (auditarLog.FechaCreacion != default)
			query = query.Where(x => x.FechaCreacion == auditarLog.FechaCreacion);

		if (auditarLog.PerformanceScore.HasValue)
			query = query.Where(x => x.PerformanceScore == auditarLog.PerformanceScore);

		if (auditarLog.TimeToFirstByteMs.HasValue)
			query = query.Where(x => x.TimeToFirstByteMs == auditarLog.TimeToFirstByteMs);

		if (auditarLog.DomProcessingTimeMs.HasValue)
			query = query.Where(x => x.DomProcessingTimeMs == auditarLog.DomProcessingTimeMs);

		if (auditarLog.PageLoadTimeMs.HasValue)
			query = query.Where(x => x.PageLoadTimeMs == auditarLog.PageLoadTimeMs);

		if (!string.IsNullOrEmpty(auditarLog.FcpValue))
			query = query.Where(x => x.FcpValue == auditarLog.FcpValue);

		if (!string.IsNullOrEmpty(auditarLog.LcpValue))
			query = query.Where(x => x.LcpValue == auditarLog.LcpValue);

		if (!string.IsNullOrEmpty(auditarLog.ClsValue))
			query = query.Where(x => x.ClsValue == auditarLog.ClsValue);

		if (!string.IsNullOrEmpty(auditarLog.SpeedIndexValue))
			query = query.Where(x => x.SpeedIndexValue == auditarLog.SpeedIndexValue);

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