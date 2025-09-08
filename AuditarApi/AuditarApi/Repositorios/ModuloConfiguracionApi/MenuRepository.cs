using Dominio.Data;
using Dominio.Entities;
using Dominio.ModuloConfiguracion.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _context;
    public MenuRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Menu> GetByMenuAsync(bool? EstadoMenu = null)
    {
        return _context.Menus
                       .Where(r => r.EstadoMenu == EstadoMenu)
                       .ToList();
    }
}
