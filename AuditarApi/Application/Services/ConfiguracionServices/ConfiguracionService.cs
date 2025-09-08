
using Dominio.Entities;
using Dominio.ModuloConfiguracion.Repositorio;
using System;

public class ConfiguracionServices
{
    #region Atributos
    private readonly IMenuRepository _menuRepository;
    #endregion

    #region constructores
    public ConfiguracionServices(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }
    #endregion


    #region Menus
    private List<MenuVista> MapToMenuVista(List<Menu> menus)
    {
        return menus.Select(r => new MenuVista
        {
            Id = r.MenuId,
            Nombre = r.NombreMenu,
            Url = r.UrlMenu,
            Icono = r.IconoMenu,
            Estado = r.EstadoMenu

        }).ToList();
    }

    public List<MenuVista>? MenuActivo(bool? Estado = null)
    {
        List<Menu> menu = _menuRepository.GetByMenuAsync(Estado);
        if (menu == null || !menu.Any())
        {
            return null;
        }

        List<MenuVista> menuVista = MapToMenuVista(menu);

        return menuVista;
    }
    #endregion
}
