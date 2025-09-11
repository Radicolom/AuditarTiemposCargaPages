using Dominio.ModuloSeguridad.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Authorize]
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
        var respuesta = _seguridadService.RolActivo(rolRequest.Estado);
        return Ok(respuesta);
    }

    [HttpPost("RolInsertar")]
    public async Task<IActionResult> RolInsertar([FromBody] RolVista rol)
    {
        var respuesta = await _seguridadService.AddRolAsync(rol);
        return Ok(respuesta);
    }

    [HttpPut("RolEditar")]
    public async Task<IActionResult> RolEditar([FromBody] RolVista rol)
    {
        var respuesta = await _seguridadService.UpdateRolAsync(rol);
        return Ok(respuesta);
    }

    [HttpDelete("RolEliminar/{rolId}")]
    public async Task<IActionResult> RolEliminar(int rolId)
    {
        var respuesta = await _seguridadService.DeleteRolAsync(rolId);
        return Ok(respuesta);
    }
    #endregion

    #region Usuarios
    [HttpPost("UsuarioObtener")]
    public async Task<IActionResult> UsuarioObtener([FromBody] UsuarioVista usuarioRequest)
    {
        var respuesta = await _seguridadService.GetByUsuarioAsync(usuarioRequest.Estado);
        return Ok(respuesta);
    }

    [HttpPost("UsuarioInsertar")]
    [AllowAnonymous]
    public async Task<IActionResult> UsuarioInsertar([FromBody] UsuarioVista usuario)
    {
        if (!string.IsNullOrEmpty(usuario.Password))
        {
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
        }
        else
        {
            usuario.Password = BCrypt.Net.BCrypt.HashPassword("Clave123+.");
        }

        var respuesta = await _seguridadService.AddUsuarioAsync(usuario);
        return Ok(respuesta);
    }

    [HttpPut("UsuarioEditar")]
    public async Task<IActionResult> UsuarioEditar([FromBody] UsuarioVista usuario)
    {
        var respuesta = await _seguridadService.UpdateUsuarioAsync(usuario);
        return Ok(respuesta);
    }

    [HttpDelete("UsuarioEliminar/{usuarioId}")]
    public async Task<IActionResult> UsuarioEliminar(int usuarioId)
    {
        var respuesta = await _seguridadService.DeleteUsuarioAsync(usuarioId);
        return Ok(respuesta);
    }
    #endregion

    #region RolOperacionAccion
    [HttpPost("RolOperacionAccionObtener")]
    public IActionResult RolOperacionAccionObtener([FromBody] RolOperacionAccionVista request)
    {
        var respuesta = _seguridadService.GetByRolOperacionAccion(request.RolId, request.ServicioId, request.AccionId);
        return Ok(respuesta);
    }

    [HttpPost("RolOperacionAccionInsertar")]
    public async Task<IActionResult> RolOperacionAccionInsertar([FromBody] RolOperacionAccionVista entity)
    {
        var respuesta = await _seguridadService.AddRolOperacionAccionAsync(entity);
        return Ok(respuesta);
    }

    [HttpPut("RolOperacionAccionEditar")]
    public async Task<IActionResult> RolOperacionAccionEditar([FromBody] RolOperacionAccionVista entity)
    {
        var respuesta = await _seguridadService.UpdateRolOperacionAccionAsync(entity);
        return Ok(respuesta);
    }

    [HttpDelete("RolOperacionAccionEliminar/{id}")]
    public async Task<IActionResult> RolOperacionAccionEliminar(int id)
    {
        var respuesta = await _seguridadService.DeleteRolOperacionAccionAsync(id);
        return Ok(respuesta);
    }
    #endregion

}