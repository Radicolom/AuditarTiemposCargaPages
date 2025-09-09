using Dominio.ModuloSeguridad.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class SeguridadController : ControllerBase
{
    private readonly SeguridadServices _seguridadService;

    public SeguridadController(SeguridadServices segurService)
    {
        _seguridadService = segurService;
    }

    #region Roles
    [HttpPost("RolObtener")]
    public IActionResult RolObtener([FromBody] RolVista rolRequest)
    {
        var roles = _seguridadService.RolActivo(rolRequest.Estado);

        if (roles == null || roles.Count == 0)
        {
            return Ok(new { message = "Sin Roles en el sistema" });
        }
        return Ok(roles);
    }

    [HttpPost("RolInsertar")]
    public async Task<IActionResult> RolInsertar([FromBody] RolVista rol)
    {
        var result = await _seguridadService.AddRolAsync(rol);
        if (result == null)
            return BadRequest(new { message = "No se pudo insertar el rol." });
        return Ok(result);
    }

    [HttpPut("RolEditar")]
    public async Task<IActionResult> RolEditar([FromBody] RolVista rol)
    {
        var result = await _seguridadService.UpdateRolAsync(rol);
        if (result == null)
            return BadRequest(new { message = "No se pudo editar el rol." });
        return Ok(result);
    }

    [HttpDelete("RolEliminar/{rolId}")]
    public async Task<IActionResult> RolEliminar(int rolId)
    {
        var eliminado = await _seguridadService.DeleteRolAsync(rolId);
        if (!eliminado)
            return NotFound(new { message = "No se encontró el rol a eliminar." });
        return Ok(new { message = "Rol eliminado correctamente." });
    }
    #endregion

    #region Usuarios
    [HttpPost("UsuarioObtener")]
    public async Task<IActionResult> UsuarioObtener([FromBody] UsuarioVista usuarioRequest)
    {
        var usuario = await _seguridadService.GetByUsuarioAsync(usuarioRequest.Estado);
        if (usuario == null)
            return Ok(new { message = "Sin usuarios en el sistema" });
        return Ok(usuario);
    }

    [HttpPost("UsuarioInsertar")]
    public async Task<IActionResult> UsuarioInsertar([FromBody] UsuarioVista usuario)
    {
        var result = await _seguridadService.AddUsuarioAsync(usuario);
        if (result == null)
            return BadRequest(new { message = "No se pudo insertar el usuario." });
        return Ok(result);
    }

    [HttpPut("UsuarioEditar")]
    public async Task<IActionResult> UsuarioEditar([FromBody] UsuarioVista usuario)
    {
        var result = await _seguridadService.UpdateUsuarioAsync(usuario);
        if (result == null)
            return BadRequest(new { message = "No se pudo editar el usuario." });
        return Ok(result);
    }

    [HttpDelete("UsuarioEliminar/{usuarioId}")]
    public async Task<IActionResult> UsuarioEliminar(int usuarioId)
    {
        var eliminado = await _seguridadService.DeleteUsuarioAsync(usuarioId);
        if (!eliminado)
            return NotFound(new { message = "No se encontró el usuario a eliminar." });
        return Ok(new { message = "Usuario eliminado correctamente." });
    }
    #endregion

    #region RolOperacionAccion
    [HttpPost("RolOperacionAccionObtener")]
    public IActionResult RolOperacionAccionObtener([FromBody] RolOperacionAccionVista request)
    {
        var result = _seguridadService.GetByRolOperacionAccion(request.RolId, request.ServicioId, request.AccionId);
        if (result == null || result.Count == 0)
            return Ok(new { message = "Sin relaciones Rol-Operación-Acción en el sistema" });
        return Ok(result);
    }

    [HttpPost("RolOperacionAccionInsertar")]
    public async Task<IActionResult> RolOperacionAccionInsertar([FromBody] RolOperacionAccionVista entity)
    {
        var result = await _seguridadService.AddRolOperacionAccionAsync(entity);
        if (result == null)
            return BadRequest(new { message = "No se pudo insertar la relación." });
        return Ok(result);
    }

    [HttpPut("RolOperacionAccionEditar")]
    public async Task<IActionResult> RolOperacionAccionEditar([FromBody] RolOperacionAccionVista entity)
    {
        var result = await _seguridadService.UpdateRolOperacionAccionAsync(entity);
        if (result == null)
            return BadRequest(new { message = "No se pudo editar la relación." });
        return Ok(result);
    }

    [HttpDelete("RolOperacionAccionEliminar/{id}")]
    public async Task<IActionResult> RolOperacionAccionEliminar(int id)
    {
        var eliminado = await _seguridadService.DeleteRolOperacionAccionAsync(id);
        if (!eliminado)
            return NotFound(new { message = "No se encontró la relación a eliminar." });
        return Ok(new { message = "Relación eliminada correctamente." });
    }
    #endregion

}