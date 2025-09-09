using Dominio.Data;
using Dominio.Entities;
using Dominio.ModuloConfiguracion.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class AccionRepository : IAccionRepository
{
    private readonly ApplicationDbContext _context;
    public AccionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Accion> GetByAccionAsync(int? menuId = null, int? accionId = null)
    {
        var query = _context.Accions.AsQueryable();

        // Eliminar el filtro por MenuId ya que la entidad Accion no tiene esa propiedad
        // if (menuId.HasValue)
        //     query = query.Where(a => a.MenuId == menuId.Value);

        if (accionId.HasValue)
            query = query.Where(a => a.AccionId == accionId.Value);

        return query.ToList();
    }

    public async Task<Accion> AddAccionAsync(Accion menu)
    {
        await _context.Accions.AddAsync(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task<Accion> UpdateAccionAsync(Accion menu)
    {
        _context.Accions.Update(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task<bool> DeleteAccionAsync(int menuId)
    {
        var menu = await _context.Accions.FindAsync(menuId);
        if (menu == null)
            return false;

        _context.Accions.Remove(menu);
        await _context.SaveChangesAsync();
        return true;
    }
}
