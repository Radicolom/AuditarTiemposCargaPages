using Dominio.Data;
using Dominio.Entities;
using Dominio.ModuloConfiguracion.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class MenuRolRepository : IMenuRolRepository
{
    private readonly ApplicationDbContext _context;
    public MenuRolRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public List<MenuRol> GetByMenuRolAsync(int? menuId = null, int? rolId = null)
    {
        return _context.MenuRols
            .Where(mr => (!menuId.HasValue || mr.MenuId == menuId) &&
                         (!rolId.HasValue || mr.RolId == rolId))
            .ToList();
    }

    public async Task<MenuRol> AddMenuRolAsync(MenuRol menuRol)
    {
        await _context.MenuRols.AddAsync(menuRol);
        await _context.SaveChangesAsync();
        return menuRol;
    }

    public async Task<MenuRol> UpdateMenuRolAsync(MenuRol menuRol)
    {
        _context.MenuRols.Update(menuRol);
        await _context.SaveChangesAsync();
        return menuRol;
    }

    public async Task<bool> DeleteMenuRolAsync(int menuRolId)
    {
        var menuRol = await _context.MenuRols.FindAsync(menuRolId);
        if (menuRol == null)
            return false;

        _context.MenuRols.Remove(menuRol);
        await _context.SaveChangesAsync();
        return true;
    }
}
