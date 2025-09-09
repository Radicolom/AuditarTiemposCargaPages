using Dominio.Data;
using Dominio.Entities;
using Dominio.ModuloConfiguracion.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ServicioRepository : IServicioRepository
{
    private readonly ApplicationDbContext _context;
    public ServicioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Servicio> GetByServicioAsync(int? servicioId = null)
    {
        var query = _context.Servicios.AsQueryable();
        if (servicioId.HasValue)
        {
            query = query.Where(s => s.ServicioId == servicioId.Value);
        }
        return query.ToList();
    }

    public async Task<Servicio> AddServicioAsync(Servicio menu)
    {
        await _context.Servicios.AddAsync(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task<Servicio> UpdateServicioAsync(Servicio menu)
    {
        _context.Servicios.Update(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task<bool> DeleteServicioAsync(int menuId)
    {
        var menu = await _context.Servicios.FindAsync(menuId);
        if (menu == null)
            return false;

        _context.Servicios.Remove(menu);
        await _context.SaveChangesAsync();
        return true;
    }
}
