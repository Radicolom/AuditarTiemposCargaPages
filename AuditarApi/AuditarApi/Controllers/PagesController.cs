using Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PagesController : ControllerBase
{
    private readonly PagesServices _pagesServices;

    public PagesController(PagesServices pagesServices)
    {
        _pagesServices = pagesServices;
    }

    #region AuditarPagina

    [HttpPost("AuditarPaginaObtener")]
    public IActionResult AuditarPaginaObtener([FromBody] AuditarPaginaVista request)
    {
        var rol = User.FindFirst("rol")?.Value;
        var respuesta = _pagesServices.AuditarPaginaObtener(request.Id);
        return Ok(respuesta);
    }

    [HttpPost("AuditarPaginaInsertar")]
    public async Task<IActionResult> AuditarPaginaInsertar([FromBody] AuditarPaginaVista pagina)
    {
        var userIdString = User.FindFirst("usuarioId")?.Value;
        if (int.TryParse(userIdString, out int userId))
        {
            pagina.UsuarioId = userId;
        }
        else
        {
            pagina.UsuarioId = null;
        }
        var respuesta = await _pagesServices.AddAuditarPaginaAsync(pagina);
        return Ok(respuesta);
    }

    [HttpPut("AuditarPaginaEditar")]
    public async Task<IActionResult> AuditarPaginaEditar([FromBody] AuditarPaginaVista pagina)
    {
        var userIdString = User.FindFirst("usuarioId")?.Value;
        if (int.TryParse(userIdString, out int userId))
        {
            pagina.UsuarioId = userId;
        }
        else
        {
            pagina.UsuarioId = null;
        }
        var respuesta = await _pagesServices.UpdateAuditarPaginaAsync(pagina);
        return Ok(respuesta);
    }

    [HttpDelete("AuditarPaginaEliminar/{id}")]
    public async Task<IActionResult> AuditarPaginaEliminar(int id)
    {
        var rol = User.FindFirst("rol")?.Value;
        var respuesta = await _pagesServices.DeleteAuditarPaginaAsync(id);
        return Ok(respuesta);
    }

    #endregion

    #region AuditarLog

    [HttpPost("AuditarLogObtener")]
    public IActionResult AuditarLogObtener([FromBody] AuditarLogVista request)
    {
        var respuesta = _pagesServices.AuditarLogObtener(request);
        return Ok(respuesta);
    }

    [HttpPost("AuditarLogInsertar")]
    public async Task<IActionResult> AuditarLogInsertar([FromBody] AuditarLogVista log)
    {
        var respuesta = await _pagesServices.AddAuditarLogAsync(log);
        return Ok(respuesta);
    }

    [HttpPut("AuditarLogEditar")]
    public async Task<IActionResult> AuditarLogEditar([FromBody] AuditarLogVista log)
    {
        var respuesta = await _pagesServices.UpdateAuditarLogAsync(log);
        return Ok(respuesta);
    }

    [HttpDelete("AuditarLogEliminar/{id}")]
    public async Task<IActionResult> AuditarLogEliminar(int id)
    {
        var respuesta = await _pagesServices.DeleteAuditarLogAsync(id);
        return Ok(respuesta);
    }

    #endregion
}