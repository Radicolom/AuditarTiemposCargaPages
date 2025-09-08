
using Dominio.Entities;
using Dominio.ModuloSeguridad.Repositorio;

public class SeguridadServices
{
    #region Atributos
    private readonly IRolRepository _rolRepository;
    #endregion

    #region constructores
    public SeguridadServices(IRolRepository rolRepository)
    {
        _rolRepository = rolRepository;
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
    #endregion
}
