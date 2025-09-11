using Dominio.Entities;
using Dominio.ModuloSeguridad.Repositorio;
using System.Collections.Generic;

public class SeguridadServices
{
    #region Atributos
    private readonly IRolRepository _rolRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IRolOperacionAccionRepository _rolOperacionAccionRepository;
    #endregion

    #region constructores
    public SeguridadServices(
        IRolRepository rolRepository,
        IUsuarioRepository usuarioRepository,
        IRolOperacionAccionRepository rolOperacionAccionRepository)
    {
        _rolRepository = rolRepository;
        _usuarioRepository = usuarioRepository;
        _rolOperacionAccionRepository = rolOperacionAccionRepository;
    }
    #endregion

    #region Roles
    private List<RolVista> MapToRolVista(List<Rol> roles)
    {
        return roles.Select(r => new RolVista
        {
            Id = r.RolId,
            Nombre = r.NombreRol,
            Estado = r.EstadoRol
        }).ToList();
    }

    private Rol MapToRol(RolVista rol)
    {
        return new Rol
        {
            RolId = rol.Id ?? 0,
            NombreRol = rol.Nombre ?? string.Empty,
            EstadoRol = rol.Estado
        };
    }

    public RespuestaApp<RolVista> RolActivo(bool? Estado = null)
    {
        var respuesta = new RespuestaApp<RolVista>();
        try
        {
            List<Rol> rol = _rolRepository.GetByRolAsync(Estado);
            respuesta.Vista = MapToRolVista(rol);
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

    public async Task<RespuestaApp<RolVista>> AddRolAsync(RolVista rol)
    {
        var respuesta = new RespuestaApp<RolVista>();
        try
        {
            Rol rolEntity = MapToRol(rol);
            var result = await _rolRepository.AddRolAsync(rolEntity);
            if (result != null)
            {
                respuesta.Vista.Add(new RolVista
                {
                    Id = result.RolId,
                    Nombre = result.NombreRol,
                    Estado = result.EstadoRol
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

    public async Task<RespuestaApp<RolVista>> UpdateRolAsync(RolVista rol)
    {
        var respuesta = new RespuestaApp<RolVista>();
        try
        {
            Rol rolEntity = MapToRol(rol);
            var result = await _rolRepository.UpdateRolAsync(rolEntity);
            if (result != null)
            {
                respuesta.Vista.Add(new RolVista
                {
                    Id = result.RolId,
                    Nombre = result.NombreRol,
                    Estado = result.EstadoRol
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

    public async Task<RespuestaApp<bool>> DeleteRolAsync(int rolId)
    {
        var respuesta = new RespuestaApp<bool>();
        try
        {
            var eliminado = await _rolRepository.DeleteRolAsync(rolId);
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

    #region Usuarios
    private List<UsuarioVista> MapToUsuarioVista(List<Usuario> usuarios)
    {
        return usuarios.Select(u => new UsuarioVista
        {
            Id = u.UsuarioId,
            Nombre = u.NombreUsuario,
            Apellido = u.ApellidoUsuario,
            Documento = u.DocumentoUsuario,
            Correo = u.CorreoUsuario,
            EmailConfirmado = u.EmailConfirmed,
            Telefono = u.TelefonoUsuario,
            TelefonoConfirmado = u.TelefonoConfirmadoUsuario,
            DobleFactor = u.AutenticacionDobleFactor,
            Intentos = u.AutenticacionIntentos,
            Estado = u.EstadoUsuario,
            RolId = u.RolId,
            RolNombre = u.Rol?.NombreRol
        }).ToList();
    }

    private Usuario MapToUsuario(UsuarioVista usuarioVista)
    {
        return new Usuario
        {
            UsuarioId = usuarioVista.Id ?? 0,
            NombreUsuario = usuarioVista.Nombre ?? string.Empty,
            ApellidoUsuario = usuarioVista.Apellido ?? string.Empty,
            DocumentoUsuario = usuarioVista.Documento ?? string.Empty,
            CorreoUsuario = usuarioVista.Correo ?? string.Empty,
            EmailConfirmed = usuarioVista.EmailConfirmado,
            TelefonoUsuario = usuarioVista.Telefono,
            TelefonoConfirmadoUsuario = usuarioVista.TelefonoConfirmado,
            AutenticacionDobleFactor = usuarioVista.DobleFactor,
            AutenticacionIntentos = usuarioVista.Intentos,
            EstadoUsuario = usuarioVista.Estado,
            RolId = usuarioVista.RolId,
            PasswordUsuario = usuarioVista.Password ?? string.Empty
        };
    }

    public async Task<RespuestaApp<UsuarioVista>> GetByUsuarioAsync(bool? estado)
    {
        var respuesta = new RespuestaApp<UsuarioVista>();
        try
        {
            List<Usuario> usuario = await Task.Run(() => _usuarioRepository.GetByUsuarioAsync(estado));
            respuesta.Vista = MapToUsuarioVista(usuario);
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

    public async Task<RespuestaApp<UsuarioVista>> AddUsuarioAsync(UsuarioVista usuario)
    {
        var respuesta = new RespuestaApp<UsuarioVista>();
        try
        {
            Usuario usuarioEntity = MapToUsuario(usuario);

            usuarioEntity.AutenticacionDobleFactor = false;
            usuarioEntity.AutenticacionIntentos = 0;
            usuarioEntity.EmailConfirmed = false;
            usuarioEntity.TelefonoConfirmadoUsuario = false;

            if (string.IsNullOrEmpty(usuarioEntity.NombreUsuario) ||
                string.IsNullOrEmpty(usuarioEntity.ApellidoUsuario) ||
                string.IsNullOrEmpty(usuarioEntity.CorreoUsuario) ||
                string.IsNullOrEmpty(usuarioEntity.DocumentoUsuario) ||
                string.IsNullOrEmpty(usuarioEntity.RolId.ToString()))
            {
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = true;
                respuesta.Mensaje = "Datos obligatorios faltantes.";
                return respuesta;
            }

            Usuario? result = await _usuarioRepository.AddUsuarioAsync(usuarioEntity);

            var usuarioVista = MapToUsuarioVista(new List<Usuario> { result }).FirstOrDefault();
            if (usuarioVista != null)
            {
                respuesta.Vista.Add(usuarioVista);
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

    public async Task<RespuestaApp<UsuarioVista>> UpdateUsuarioAsync(UsuarioVista usuario)
    {
        var respuesta = new RespuestaApp<UsuarioVista>();
        try
        {
            Usuario usuarioEntity = MapToUsuario(usuario);
            Usuario? result = await _usuarioRepository.UpdateUsuarioAsync(usuarioEntity);
            if (result != null)
            {
                respuesta.Vista.Add(MapToUsuarioVista(new List<Usuario> { result }).FirstOrDefault());
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

    public async Task<RespuestaApp<bool>> DeleteUsuarioAsync(int usuarioId)
    {
        var respuesta = new RespuestaApp<bool>();
        try
        {
            var eliminado = await _usuarioRepository.DeleteUsuarioAsync(usuarioId);
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

    #region RolOperacionAccion
    private RolOperacionAccionVista MapToRolOperacionAccionVista(RolOperacionAccion entity)
    {
        return new RolOperacionAccionVista
        {
            Id = entity.RolOperacionAccionId,
            RolId = entity.RolId,
            RolNombre = entity.Rol?.NombreRol,
            ServicioId = entity.ServicioId,
            AccionId = entity.AccionId,
            AccionNombre = entity.Accion?.NombreAccion,
        };
    }
    private RolOperacionAccion MapToRolOperacionAccion(RolOperacionAccionVista vista)
    {
        return new RolOperacionAccion
        {
            RolOperacionAccionId = vista.Id ?? 0,
            RolId = vista.RolId ?? 0,
            ServicioId = vista.ServicioId ?? 0,
            AccionId = vista.AccionId ?? 0,
        };
    }

    public RespuestaApp<RolOperacionAccionVista> GetByRolOperacionAccion(int? rolId = null, int? operacionId = null, int? accionId = null)
    {
        var respuesta = new RespuestaApp<RolOperacionAccionVista>();
        try
        {
            var entities = _rolOperacionAccionRepository.GetByRolOperacionAccionAsync(rolId, operacionId, accionId);
            respuesta.Vista = entities?.Select(MapToRolOperacionAccionVista).ToList() ?? new List<RolOperacionAccionVista>();
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

    public async Task<RespuestaApp<RolOperacionAccionVista>> AddRolOperacionAccionAsync(RolOperacionAccionVista entity)
    {
        var respuesta = new RespuestaApp<RolOperacionAccionVista>();
        try
        {
            RolOperacionAccion entityToAdd = MapToRolOperacionAccion(entity);
            var result = await _rolOperacionAccionRepository.AddRolOperacionAccionAsync(entityToAdd);
            if (result != null)
            {
                respuesta.Vista.Add(MapToRolOperacionAccionVista(result));
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

    public async Task<RespuestaApp<RolOperacionAccionVista>> UpdateRolOperacionAccionAsync(RolOperacionAccionVista entity)
    {
        var respuesta = new RespuestaApp<RolOperacionAccionVista>();
        try
        {
            RolOperacionAccion entityToUpdate = MapToRolOperacionAccion(entity);
            var result = await _rolOperacionAccionRepository.UpdateRolOperacionAccionAsync(entityToUpdate);
            if (result != null)
            {
                respuesta.Vista.Add(MapToRolOperacionAccionVista(result));
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

    public async Task<RespuestaApp<bool>> DeleteRolOperacionAccionAsync(int id)
    {
        var respuesta = new RespuestaApp<bool>();
        try
        {
            var eliminado = await _rolOperacionAccionRepository.DeleteRolOperacionAccionAsync(id);
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
