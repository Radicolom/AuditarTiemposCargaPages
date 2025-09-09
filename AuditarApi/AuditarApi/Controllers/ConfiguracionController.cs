using Dominio.ModuloConfiguracion.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ConfiguracionController : ControllerBase
{
    private readonly ConfiguracionServices _configuracionService;

    public ConfiguracionController(ConfiguracionServices segurService)
    {
        _configuracionService = segurService;
    }

    #region Menus
    [HttpPost("MenuObtener")]
    public IActionResult MenuObtener([FromBody] RolVista rolRequest)
    {
        var menus = _configuracionService.MenuActivo(rolRequest.Estado);

        if (menus == null || menus.Count == 0)
        {
            return Ok(new { message = "Sin Roles en el sistema" });
        }
        return Ok(menus);
    }

    [HttpPost("MenuInsertar")]
    public async Task<IActionResult> MenuInsertar([FromBody] MenuVista menu)
    {
        var result = await _configuracionService.AddMenuAsync(menu);
        if (result == null)
            return BadRequest(new { message = "No se pudo insertar el menú." });
        return Ok(result);
    }

    [HttpPut("MenuEditar")]
    public async Task<IActionResult> MenuEditar([FromBody] MenuVista menu)
    {
        var result = await _configuracionService.UpdateMenuAsync(menu);
        if (result == null)
            return BadRequest(new { message = "No se pudo editar el menú." });
        return Ok(result);
    }

    [HttpDelete("MenuEliminar/{menuId}")]
    public async Task<IActionResult> MenuEliminar(int menuId)
    {
        var eliminado = await _configuracionService.DeleteMenuAsync(menuId);
        if (!eliminado)
            return NotFound(new { message = "No se encontró el menú a eliminar." });
        return Ok(new { message = "Menú eliminado correctamente." });
    }
    #endregion

    #region MenuRol
    [HttpPost("MenuRolObtener")]
    public IActionResult MenuRolObtener([FromBody] MenuRolVista menuRolRequest)
    {
        var menuRoles = _configuracionService.MenuRolActivo(menuRolRequest.MenuId, menuRolRequest.RolId);

        if (menuRoles == null || menuRoles.Count == 0)
        {
            return Ok(new { message = "Sin relaciones menú-rol en el sistema" });
        }
        return Ok(menuRoles);
    }

    [HttpPost("MenuRolInsertar")]
    public async Task<IActionResult> MenuRolInsertar([FromBody] MenuRolVista menuRol)
    {
        var result = await _configuracionService.AddMenuRolAsync(menuRol);
        if (result == null)
            return BadRequest(new { message = "No se pudo insertar la relación menú-rol." });
        return Ok(result);
    }

    [HttpPut("MenuRolEditar")]
    public async Task<IActionResult> MenuRolEditar([FromBody] MenuRolVista menuRol)
    {
        var result = await _configuracionService.UpdateMenuRolAsync(menuRol);
        if (result == null)
            return BadRequest(new { message = "No se pudo editar la relación menú-rol." });
        return Ok(result);
    }

    [HttpDelete("MenuRolEliminar/{menuRolId}")]
    public async Task<IActionResult> MenuRolEliminar(int menuRolId)
    {
        var eliminado = await _configuracionService.DeleteMenuRolAsync(menuRolId);
        if (!eliminado)
            return NotFound(new { message = "No se encontró la relación menú-rol a eliminar." });
        return Ok(new { message = "Relación menú-rol eliminada correctamente." });
    }
    #endregion

    #region Accion
    [HttpPost("AccionObtener")]
    public IActionResult AccionObtener([FromBody] AccionVista accionRequest)
    {
        var acciones = _configuracionService.AccionActivo(accionRequest.Id);
        if (acciones == null || acciones.Count == 0)
        {
            return Ok(new { message = "Sin acciones en el sistema" });
        }
        return Ok(acciones);
    }

    [HttpPost("AccionInsertar")]
    public async Task<IActionResult> AccionInsertar([FromBody] AccionVista accion)
    {
        var result = await _configuracionService.AddAccionAsync(accion);
        if (result == null)
            return BadRequest(new { message = "No se pudo insertar la acción." });
        return Ok(result);
    }

    [HttpPut("AccionEditar")]
    public async Task<IActionResult> AccionEditar([FromBody] AccionVista accion)
    {
        var result = await _configuracionService.UpdateAccionAsync(accion);
        if (result == null)
            return BadRequest(new { message = "No se pudo editar la acción." });
        return Ok(result);
    }

    [HttpDelete("AccionEliminar/{accionId}")]
    public async Task<IActionResult> AccionEliminar(int accionId)
    {
        var eliminado = await _configuracionService.DeleteAccionAsync(accionId);
        if (!eliminado)
            return NotFound(new { message = "No se encontró la acción a eliminar." });
        return Ok(new { message = "Acción eliminada correctamente." });
    }
    #endregion

    #region Servicio
    [HttpPost("ServicioObtener")]
    public IActionResult ServicioObtener([FromBody] ServicioVista servicioRequest)
    {
        var servicios = _configuracionService.ServicioActivo(servicioRequest.Id);
        if (servicios == null || servicios.Count == 0)
        {
            return Ok(new { message = "Sin servicios en el sistema" });
        }
        return Ok(servicios);
    }

    [HttpPost("ServicioInsertar")]
    public async Task<IActionResult> ServicioInsertar([FromBody] ServicioVista servicio)
    {
        var result = await _configuracionService.AddServicioAsync(servicio);
        if (result == null)
            return BadRequest(new { message = "No se pudo insertar el servicio." });
        return Ok(result);
    }

    [HttpPut("ServicioEditar")]
    public async Task<IActionResult> ServicioEditar([FromBody] ServicioVista servicio)
    {
        var result = await _configuracionService.UpdateServicioAsync(servicio);
        if (result == null)
            return BadRequest(new { message = "No se pudo editar el servicio." });
        return Ok(result);
    }

    [HttpDelete("ServicioEliminar/{servicioId}")]
    public async Task<IActionResult> ServicioEliminar(int servicioId)
    {
        var eliminado = await _configuracionService.DeleteServicioAsync(servicioId);
        if (!eliminado)
            return NotFound(new { message = "No se encontró el servicio a eliminar." });
        return Ok(new { message = "Servicio eliminado correctamente." });
    }
    #endregion
}