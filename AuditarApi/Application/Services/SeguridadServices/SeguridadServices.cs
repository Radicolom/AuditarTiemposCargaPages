using Dominio.Entities;
using Dominio.ModuloSeguridad.Repositorio;
using System.Collections.Generic;
using System.Data;

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

    public List<RolVista>? RolActivo(bool? Estado = null)
    {
        List<Rol> rol = _rolRepository.GetByRolAsync(Estado);
        if (rol == null || !rol.Any())
        {
            return null;
        }

        List<RolVista> rolVista = MapToRolVista(rol);

        return rolVista;
    }

    public async Task<RolVista?> AddRolAsync(RolVista rol)
    {
        Rol rolEntity = MapToRol(rol);
        var result = await _rolRepository.AddRolAsync(rolEntity);
        if (result == null) return null;
        return new RolVista
        {
            Id = result.RolId,
            Nombre = result.NombreRol,
            Estado = result.EstadoRol
        };
    }

    public async Task<RolVista?> UpdateRolAsync(RolVista rol)
    {
        Rol rolEntity = MapToRol(rol);
        var result = await _rolRepository.UpdateRolAsync(rolEntity);
        if (result == null) return null;
        return new RolVista
        {
            Id = result.RolId,
            Nombre = result.NombreRol,
            Estado = result.EstadoRol
        };
    }

    public async Task<bool> DeleteRolAsync(int rolId)
    {
        return await _rolRepository.DeleteRolAsync(rolId);
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
            RolId = usuarioVista.RolId
        };
    }

    public async Task<List<UsuarioVista>?> GetByUsuarioAsync(bool? estado)
    {
        List<Usuario> usuario = await Task.Run(() => _usuarioRepository.GetByUsuarioAsync(estado));
        if (usuario == null || !usuario.Any())
        {
            return null;
        }

        List<UsuarioVista> usuarioVistas = MapToUsuarioVista(usuario);

        return usuarioVistas;
    }

    public async Task<UsuarioVista?> AddUsuarioAsync(UsuarioVista usuario)
    {
        Usuario usuarioEntity = MapToUsuario(usuario);
        Usuario? result = await _usuarioRepository.AddUsuarioAsync(usuarioEntity);
        if (result == null) return null;
        List<Usuario> usuarioVistas = new List<Usuario> { result };

        UsuarioVista? usuarioVista = MapToUsuarioVista(usuarioVistas).FirstOrDefault();

        return usuarioVista;
    }

    public async Task<UsuarioVista?> UpdateUsuarioAsync(UsuarioVista usuario)
    {
        Usuario usuarioEntity = MapToUsuario(usuario);
        Usuario? result = await _usuarioRepository.UpdateUsuarioAsync(usuarioEntity);
        if (result == null) return null;
        List<Usuario> usuarioVistas = new List<Usuario> { result };

        UsuarioVista? usuarioVista = MapToUsuarioVista(usuarioVistas).FirstOrDefault();

        return usuarioVista;
    }

    public async Task<bool> DeleteUsuarioAsync(int usuarioId)
    {
        return await _usuarioRepository.DeleteUsuarioAsync(usuarioId);
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
    public List<RolOperacionAccionVista>? GetByRolOperacionAccion(int? rolId = null, int? operacionId = null, int? accionId = null)
    {
        var entities = _rolOperacionAccionRepository.GetByRolOperacionAccionAsync(rolId, operacionId, accionId);
        if (entities == null || !entities.Any())
        {
            return null;
        }
        return entities.Select(MapToRolOperacionAccionVista).ToList();
    }
    public async Task<RolOperacionAccionVista?> AddRolOperacionAccionAsync(RolOperacionAccionVista entity)
    {
        RolOperacionAccion entityToAdd = MapToRolOperacionAccion(entity);
        var result = await _rolOperacionAccionRepository.AddRolOperacionAccionAsync(entityToAdd);
        if (result == null) return null;
        return MapToRolOperacionAccionVista(result);
    }
    public async Task<RolOperacionAccionVista?> UpdateRolOperacionAccionAsync(RolOperacionAccionVista entity)
    {
        RolOperacionAccion entityToUpdate = MapToRolOperacionAccion(entity);
        var result = await _rolOperacionAccionRepository.UpdateRolOperacionAccionAsync(entityToUpdate);
        if (result == null) return null;
        return MapToRolOperacionAccionVista(result);
    }
    public async Task<bool> DeleteRolOperacionAccionAsync(int id)
    {
        return await _rolOperacionAccionRepository.DeleteRolOperacionAccionAsync(id);
    }
    #endregion
}
