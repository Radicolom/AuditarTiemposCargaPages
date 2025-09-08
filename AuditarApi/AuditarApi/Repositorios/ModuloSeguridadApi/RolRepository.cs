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
}
