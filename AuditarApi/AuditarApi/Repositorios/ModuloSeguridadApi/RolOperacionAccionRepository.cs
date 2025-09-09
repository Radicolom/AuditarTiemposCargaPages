using Dominio.Data;
using Dominio.Entities;
using Dominio.ModuloSeguridad.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class RolOperacionAccionRepository : IRolOperacionAccionRepository
{
    private readonly ApplicationDbContext _context;
    public RolOperacionAccionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<RolOperacionAccion> GetByRolOperacionAccionAsync(int? rolId = null, int? operacionId = null, int? accionId = null)
    {
        var query = _context.RolOperacionAccions.AsQueryable();

        if (rolId.HasValue)
            query = query.Where(r => r.RolId == rolId.Value);

        if (operacionId.HasValue)
            query = query.Where(r => r.ServicioId == operacionId.Value);

        if (accionId.HasValue)
            query = query.Where(r => r.AccionId == accionId.Value);

        return query.ToList();
    }

    public async Task<RolOperacionAccion> AddRolOperacionAccionAsync(RolOperacionAccion rolOperacionAccion)
    {
        await _context.RolOperacionAccions.AddAsync(rolOperacionAccion);
        await _context.SaveChangesAsync();
        return rolOperacionAccion;
    }

    public async Task<RolOperacionAccion> UpdateRolOperacionAccionAsync(RolOperacionAccion rolOperacionAccion)
    {
        _context.RolOperacionAccions.Update(rolOperacionAccion);
        await _context.SaveChangesAsync();
        return rolOperacionAccion;
    }

    public async Task<bool> DeleteRolOperacionAccionAsync(int rolOperacionAccionId)
    {
        var entity = await _context.RolOperacionAccions.FindAsync(rolOperacionAccionId);
        if (entity == null)
            return false;

        _context.RolOperacionAccions.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
