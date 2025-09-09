#region Usings
using Dominio.Entities;
using Dominio.ModuloConfiguracion.Repositorio;
using System;
#endregion

public class ConfiguracionServices
{
    #region Atributos
    private readonly IMenuRepository _menuRepository;
    private readonly IMenuRolRepository _menuRolRepository;
    private readonly IAccionRepository _accionRepository;
    private readonly IServicioRepository _servicioRepository;
    #endregion

    #region constructores
    public ConfiguracionServices(
        IMenuRepository menuRepository,
        IMenuRolRepository menuRolRepository,
        IAccionRepository accionRepository,
        IServicioRepository servicioRepository)
    {
        _menuRepository = menuRepository;
        _menuRolRepository = menuRolRepository;
        _accionRepository = accionRepository;
        _servicioRepository = servicioRepository;
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

    private Menu MapToMenu(MenuVista menuVista)
    {
        return new Menu
        {
            MenuId = menuVista.Id ?? throw new InvalidOperationException("El Id de MenuVista no puede ser nulo."),
            NombreMenu = menuVista.Nombre ?? throw new InvalidOperationException("El Id de MenuVista no puede ser nulo."),
            UrlMenu = menuVista.Url,
            IconoMenu = menuVista.Icono,
            EstadoMenu = menuVista.Estado
        };
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

    public async Task<MenuVista?> AddMenuAsync(MenuVista menu)
    {
        Menu menuEntity = MapToMenu(menu);
        var result = await _menuRepository.AddMenuAsync(menuEntity);
        if (result == null) return null;
        return new MenuVista
        {
            Id = result.MenuId,
            Nombre = result.NombreMenu,
            Url = result.UrlMenu,
            Icono = result.IconoMenu,
            Estado = result.EstadoMenu
        };
    }

    public async Task<MenuVista?> UpdateMenuAsync(MenuVista menu)
    {
        Menu menuEntity = MapToMenu(menu);
        var result = await _menuRepository.UpdateMenuAsync(menuEntity);
        if (result == null) return null;
        return new MenuVista
        {
            Id = result.MenuId,
            Nombre = result.NombreMenu,
            Url = result.UrlMenu,
            Icono = result.IconoMenu,
            Estado = result.EstadoMenu
        };
    }

    public async Task<bool> DeleteMenuAsync(int menuId)
    {
        return await _menuRepository.DeleteMenuAsync(menuId);
    }
    #endregion

    #region MenuRol
    private List<MenuRolVista> MapToMenuRolVista(List<MenuRol> menuRols)
    {
        return menuRols.Select(r => new MenuRolVista
        {
            Id = r.MenuRolId,
            MenuId = r.MenuId,
            MenuNombre = r.Menu?.NombreMenu,
            RolId = r.RolId,
            RolNombre = r.Rol?.NombreRol
        }).ToList();
    }
    private MenuRol MapToMenuRol(MenuRolVista menuRolVista)
    {
        return new MenuRol
        {
            MenuRolId = menuRolVista.Id ?? throw new InvalidOperationException("El Id de MenuRolVista no puede ser nulo."),
            MenuId = menuRolVista.MenuId ?? throw new InvalidOperationException("El MenuId de MenuRolVista no puede ser nulo."),
            RolId = menuRolVista.RolId ?? throw new InvalidOperationException("El RolId de MenuRolVista no puede ser nulo.")
        };
    }

    public List<MenuRolVista>? MenuRolActivo(int? menuId = null, int? rolId = null)
    {
        var menuRols = _menuRolRepository.GetByMenuRolAsync(menuId, rolId);
        if (menuRols == null || !menuRols.Any())
        {
            return null;
        }
        return MapToMenuRolVista(menuRols);
    }

    public async Task<MenuRolVista?> AddMenuRolAsync(MenuRolVista menuRol)
    {
        MenuRol menuRolEntity = MapToMenuRol(menuRol);
        var result = await _menuRolRepository.AddMenuRolAsync(menuRolEntity);
        if (result == null) return null;
        return new MenuRolVista
        {
            Id = result.MenuRolId,
            MenuId = result.MenuId,
            MenuNombre = result.Menu?.NombreMenu,
            RolId = result.RolId,
            RolNombre = result.Rol?.NombreRol
        };
    }

    public async Task<MenuRolVista?> UpdateMenuRolAsync(MenuRolVista menuRol)
    {
        MenuRol menuRolEntity = MapToMenuRol(menuRol);
        var result = await _menuRolRepository.UpdateMenuRolAsync(menuRolEntity);
        if (result == null) return null;
        return new MenuRolVista
        {
            Id = result.MenuRolId,
            MenuId = result.MenuId,
            MenuNombre = result.Menu?.NombreMenu,
            RolId = result.RolId,
            RolNombre = result.Rol?.NombreRol
        };
    }

    public async Task<bool> DeleteMenuRolAsync(int menuRolId)
    {
        return await _menuRolRepository.DeleteMenuRolAsync(menuRolId);
    }
    #endregion

    #region Accion
    private List<AccionVista> MapToAccionVista(List<Accion> acciones)
    {
        return acciones.Select(a => new AccionVista
        {
            Id = a.AccionId,
            Nombre = a.NombreAccion
        }).ToList();
    }

    private Accion MapToAccion(AccionVista accionVista)
    {
        return new Accion
        {
            AccionId = accionVista.Id ?? throw new InvalidOperationException("El Id de AccionVista no puede ser nulo."),
            NombreAccion = accionVista.Nombre ?? throw new InvalidOperationException("El Nombre de AccionVista no puede ser nulo.")
        };
    }

    public List<AccionVista>? AccionActivo(int? accionId = null)
    {
        var acciones = _accionRepository.GetByAccionAsync(accionId);
        if (acciones == null || !acciones.Any())
        {
            return null;
        }
        return MapToAccionVista(acciones);
    }

    public async Task<AccionVista?> AddAccionAsync(AccionVista accion)
    {
        Accion accionEntity = MapToAccion(accion);
        var result = await _accionRepository.AddAccionAsync(accionEntity);
        if (result == null) return null;
        return new AccionVista
        {
            Id = result.AccionId,
            Nombre = result.NombreAccion
        };
    }

    public async Task<AccionVista?> UpdateAccionAsync(AccionVista accion)
    {
        Accion accionEntity = MapToAccion(accion);
        var result = await _accionRepository.UpdateAccionAsync(accionEntity);
        if (result == null) return null;
        return new AccionVista
        {
            Id = result.AccionId,
            Nombre = result.NombreAccion
        };
    }

    public async Task<bool> DeleteAccionAsync(int accionId)
    {
        return await _accionRepository.DeleteAccionAsync(accionId);
    }
    #endregion

    #region Servicio
    private List<ServicioVista> MapToServicioVista(List<Servicio> servicios)
    {
        return servicios.Select(s => new ServicioVista
        {
            Id = s.ServicioId,
            Nombre = s.NombreServicio
        }).ToList();
    }
    private Servicio MapToServicio(ServicioVista servicioVista)
    {
        return new Servicio
        {
            ServicioId = servicioVista.Id ?? throw new InvalidOperationException("El Id de ServicioVista no puede ser nulo."),
            NombreServicio = servicioVista.Nombre ?? throw new InvalidOperationException("El Nombre de ServicioVista no puede ser nulo.")
        };
    }

    public List<ServicioVista>? ServicioActivo(int? servicioId = null)
    {
        var servicios = _servicioRepository.GetByServicioAsync(servicioId);
        if (servicios == null || !servicios.Any())
        {
            return null;
        }
        return MapToServicioVista(servicios);
    }

    public async Task<ServicioVista?> AddServicioAsync(ServicioVista servicio)
    {
        Servicio servicioEntity = MapToServicio(servicio);
        var result = await _servicioRepository.AddServicioAsync(servicioEntity);
        if (result == null) return null;
        return new ServicioVista
        {
            Id = result.ServicioId,
            Nombre = result.NombreServicio
        };
    }

    public async Task<ServicioVista?> UpdateServicioAsync(ServicioVista servicio)
    {
        Servicio servicioEntity = MapToServicio(servicio);
        var result = await _servicioRepository.UpdateServicioAsync(servicioEntity);
        if (result == null) return null;
        return new ServicioVista
        {
            Id = result.ServicioId,
            Nombre = result.NombreServicio
        };
    }

    public async Task<bool> DeleteServicioAsync(int servicioId)
    {
        return await _servicioRepository.DeleteServicioAsync(servicioId);
    }
    #endregion
}
