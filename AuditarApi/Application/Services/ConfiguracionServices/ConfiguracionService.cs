#region Usings
using Dominio.Entities;
using Dominio.ModuloConfiguracion.Repositorio;
using Dominio.ModuloPages.Repositorio;
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
            NombreMenu = menuVista.Nombre ?? throw new InvalidOperationException("El Nombre de MenuVista no puede ser nulo."),
            UrlMenu = menuVista.Url,
            IconoMenu = menuVista.Icono,
            EstadoMenu = menuVista.Estado
        };
    }

    public RespuestaApp<MenuVista> MenuActivo(bool? Estado = null)
    {
        var respuesta = new RespuestaApp<MenuVista>();
        try
        {
            List<Menu> menu = _menuRepository.GetByMenuAsync(Estado);
            respuesta.Vista = MapToMenuVista(menu);
            respuesta.OperacionExitosa = true;
            respuesta.ValidacionesNegocio = false;
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<MenuVista>> AddMenuAsync(MenuVista menu)
    {
        var respuesta = new RespuestaApp<MenuVista>();
        try
        {
            Menu menuEntity = MapToMenu(menu);
            var result = await _menuRepository.AddMenuAsync(menuEntity);
            if (result != null)
            {
                respuesta.Vista.Add(new MenuVista
                {
                    Id = result.MenuId,
                    Nombre = result.NombreMenu,
                    Url = result.UrlMenu,
                    Icono = result.IconoMenu,
                    Estado = result.EstadoMenu
                });
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<MenuVista>> UpdateMenuAsync(MenuVista menu)
    {
        var respuesta = new RespuestaApp<MenuVista>();
        try
        {
            Menu menuEntity = MapToMenu(menu);
            var result = await _menuRepository.UpdateMenuAsync(menuEntity);
            if (result != null)
            {
                respuesta.Vista.Add(new MenuVista
                {
                    Id = result.MenuId,
                    Nombre = result.NombreMenu,
                    Url = result.UrlMenu,
                    Icono = result.IconoMenu,
                    Estado = result.EstadoMenu
                });
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<bool>> DeleteMenuAsync(int menuId)
    {
        var respuesta = new RespuestaApp<bool>();
        try
        {
            var eliminado = await _menuRepository.DeleteMenuAsync(menuId);
            respuesta.Vista.Add(eliminado);
            respuesta.OperacionExitosa = eliminado;
            respuesta.ValidacionesNegocio = eliminado;
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
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

    public RespuestaApp<MenuRolVista> MenuRolActivo(int? menuId = null, int? rolId = null)
    {
        var respuesta = new RespuestaApp<MenuRolVista>();
        try
        {
            var menuRols = _menuRolRepository.GetByMenuRolAsync(menuId, rolId);
            respuesta.Vista = MapToMenuRolVista(menuRols);
            respuesta.OperacionExitosa = true;
            respuesta.ValidacionesNegocio = false;
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<MenuRolVista>> AddMenuRolAsync(MenuRolVista menuRol)
    {
        var respuesta = new RespuestaApp<MenuRolVista>();
        try
        {
            MenuRol menuRolEntity = MapToMenuRol(menuRol);
            var result = await _menuRolRepository.AddMenuRolAsync(menuRolEntity);
            if (result != null)
            {
                respuesta.Vista.Add(new MenuRolVista
                {
                    Id = result.MenuRolId,
                    MenuId = result.MenuId,
                    MenuNombre = result.Menu?.NombreMenu,
                    RolId = result.RolId,
                    RolNombre = result.Rol?.NombreRol
                });
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<MenuRolVista>> UpdateMenuRolAsync(MenuRolVista menuRol)
    {
        var respuesta = new RespuestaApp<MenuRolVista>();
        try
        {
            MenuRol menuRolEntity = MapToMenuRol(menuRol);
            var result = await _menuRolRepository.UpdateMenuRolAsync(menuRolEntity);
            if (result != null)
            {
                respuesta.Vista.Add(new MenuRolVista
                {
                    Id = result.MenuRolId,
                    MenuId = result.MenuId,
                    MenuNombre = result.Menu?.NombreMenu,
                    RolId = result.RolId,
                    RolNombre = result.Rol?.NombreRol
                });
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<bool>> DeleteMenuRolAsync(int menuRolId)
    {
        var respuesta = new RespuestaApp<bool>();
        try
        {
            var eliminado = await _menuRolRepository.DeleteMenuRolAsync(menuRolId);
            respuesta.Vista.Add(eliminado);
            respuesta.OperacionExitosa = eliminado;
            respuesta.ValidacionesNegocio = eliminado;
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
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

    public RespuestaApp<AccionVista> AccionActivo(int? accionId = null)
    {
        var respuesta = new RespuestaApp<AccionVista>();
        try
        {
            var acciones = _accionRepository.GetByAccionAsync(accionId);
            respuesta.Vista = MapToAccionVista(acciones);
            respuesta.OperacionExitosa = true;
            respuesta.ValidacionesNegocio = false;
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<AccionVista>> AddAccionAsync(AccionVista accion)
    {
        var respuesta = new RespuestaApp<AccionVista>();
        try
        {
            Accion accionEntity = MapToAccion(accion);
            var result = await _accionRepository.AddAccionAsync(accionEntity);
            if (result != null)
            {
                respuesta.Vista.Add(new AccionVista
                {
                    Id = result.AccionId,
                    Nombre = result.NombreAccion
                });
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<AccionVista>> UpdateAccionAsync(AccionVista accion)
    {
        var respuesta = new RespuestaApp<AccionVista>();
        try
        {
            Accion accionEntity = MapToAccion(accion);
            var result = await _accionRepository.UpdateAccionAsync(accionEntity);
            if (result != null)
            {
                respuesta.Vista.Add(new AccionVista
                {
                    Id = result.AccionId,
                    Nombre = result.NombreAccion
                });
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<bool>> DeleteAccionAsync(int accionId)
    {
        var respuesta = new RespuestaApp<bool>();
        try
        {
            var eliminado = await _accionRepository.DeleteAccionAsync(accionId);
            respuesta.Vista.Add(eliminado);
            respuesta.OperacionExitosa = eliminado;
            respuesta.ValidacionesNegocio = eliminado;
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
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

    public RespuestaApp<ServicioVista> ServicioActivo(int? servicioId = null)
    {
        var respuesta = new RespuestaApp<ServicioVista>();
        try
        {
            var servicios = _servicioRepository.GetByServicioAsync(servicioId);
            respuesta.Vista = MapToServicioVista(servicios);
            respuesta.OperacionExitosa = true;
            respuesta.ValidacionesNegocio = false;
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<ServicioVista>> AddServicioAsync(ServicioVista servicio)
    {
        var respuesta = new RespuestaApp<ServicioVista>();
        try
        {
            Servicio servicioEntity = MapToServicio(servicio);
            var result = await _servicioRepository.AddServicioAsync(servicioEntity);
            if (result != null)
            {
                respuesta.Vista.Add(new ServicioVista
                {
                    Id = result.ServicioId,
                    Nombre = result.NombreServicio
                });
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<ServicioVista>> UpdateServicioAsync(ServicioVista servicio)
    {
        var respuesta = new RespuestaApp<ServicioVista>();
        try
        {
            Servicio servicioEntity = MapToServicio(servicio);
            var result = await _servicioRepository.UpdateServicioAsync(servicioEntity);
            if (result != null)
            {
                respuesta.Vista.Add(new ServicioVista
                {
                    Id = result.ServicioId,
                    Nombre = result.NombreServicio
                });
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<bool>> DeleteServicioAsync(int servicioId)
    {
        var respuesta = new RespuestaApp<bool>();
        try
        {
            var eliminado = await _servicioRepository.DeleteServicioAsync(servicioId);
            respuesta.Vista.Add(eliminado);
            respuesta.OperacionExitosa = eliminado;
            respuesta.ValidacionesNegocio = eliminado;
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }
    #endregion
}
