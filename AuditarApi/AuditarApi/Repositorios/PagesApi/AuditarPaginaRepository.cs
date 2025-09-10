using Dominio.Entities;
using Dominio.ModuloPages.Repositorio;
using Dominio.Data;
using Microsoft.EntityFrameworkCore;

public class AuditarPaginaRepository : IAuditarPaginaRepository
{
    private readonly ApplicationDbContext _context;

    public AuditarPaginaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<AuditarPagina> GetByAuditarPaginaAsync(int? menuId = null, int? accionId = null)
    {
        var query = _context.AuditarPaginas
            .Include(x => x.Usuario)
            .AsQueryable();

        //if (menuId.HasValue)
            //query = query.Where(x => x.MenuId == menuId.Value);

        //if (accionId.HasValue)
        //    query = query.Where(x => x.AccionId == accionId.Value);

        return query.ToList();
    }

    public async Task<AuditarPagina> AddAuditarPaginaAsync(AuditarPagina auditarPagina)
    {
        _context.AuditarPaginas.Add(auditarPagina);
        await _context.SaveChangesAsync();
        return auditarPagina;
    }

    public async Task<AuditarPagina> UpdateAuditarPaginaAsync(AuditarPagina auditarPagina)
    {
        _context.AuditarPaginas.Update(auditarPagina);
        await _context.SaveChangesAsync();
        return auditarPagina;
    }

    public async Task<bool> DeleteAuditarPaginaAsync(int auditarPaginaId)
    {
        var entity = await _context.AuditarPaginas.FindAsync(auditarPaginaId);
        if (entity == null)
            return false;

        _context.AuditarPaginas.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}