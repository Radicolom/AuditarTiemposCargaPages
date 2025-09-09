using Dominio.Data;
using Dominio.Entities;
using Dominio.ModuloSeguridad.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class RolRepository : IRolRepository
{
    private readonly ApplicationDbContext _context;
    public RolRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Rol> GetByRolAsync(bool? EstadoRol = null)
    {
        return _context.Rols
                       .Where(r => r.EstadoRol == EstadoRol)
                       .ToList();
    }

    public async Task<Rol> AddRolAsync(Rol rol)
    {
        await _context.Rols.AddAsync(rol);
        await _context.SaveChangesAsync();
        return rol;
    }

    public async Task<Rol> UpdateRolAsync(Rol rol)
    {
        _context.Rols.Update(rol);
        await _context.SaveChangesAsync();
        return rol;
    }

    public async Task<bool> DeleteRolAsync(int rolId)
    {
        var rol = await _context.Rols.FindAsync(rolId);
        if (rol == null)
            return false;
        _context.Rols.Remove(rol);
        await _context.SaveChangesAsync();
        return true;
    }
}
