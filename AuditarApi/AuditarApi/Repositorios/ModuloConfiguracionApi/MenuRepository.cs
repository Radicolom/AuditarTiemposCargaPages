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

    public async Task<Menu> AddMenuAsync(Menu menu)
    {
        await _context.Menus.AddAsync(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task<Menu> UpdateMenuAsync(Menu menu)
    {
        _context.Menus.Update(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task<bool> DeleteMenuAsync(int menuId)
    {
        var menu = await _context.Menus.FindAsync(menuId);
        if (menu == null)
            return false;

        _context.Menus.Remove(menu);
        await _context.SaveChangesAsync();
        return true;
    }
}
