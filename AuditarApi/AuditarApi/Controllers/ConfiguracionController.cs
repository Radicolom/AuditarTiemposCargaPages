using Dominio.ModuloConfiguracion.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Authorize]
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
        var respuesta = _configuracionService.MenuActivo(rolRequest.Estado);
        return Ok(respuesta);
    }

    [HttpPost("MenuInsertar")]
    public async Task<IActionResult> MenuInsertar([FromBody] MenuVista menu)
    {
        var respuesta = await _configuracionService.AddMenuAsync(menu);
        return Ok(respuesta);
    }

    [HttpPut("MenuEditar")]
    public async Task<IActionResult> MenuEditar([FromBody] MenuVista menu)
    {
        var respuesta = await _configuracionService.UpdateMenuAsync(menu);
        return Ok(respuesta);
    }

    [HttpDelete("MenuEliminar/{menuId}")]
    public async Task<IActionResult> MenuEliminar(int menuId)
    {
        var respuesta = await _configuracionService.DeleteMenuAsync(menuId);
        return Ok(respuesta);
    }
    #endregion

    #region MenuRol
    [HttpPost("MenuRolObtener")]
    public IActionResult MenuRolObtener([FromBody] MenuRolVista menuRolRequest)
    {
        var respuesta = _configuracionService.MenuRolActivo(menuRolRequest.MenuId, menuRolRequest.RolId);
        return Ok(respuesta);
    }

    [HttpPost("MenuRolInsertar")]
    public async Task<IActionResult> MenuRolInsertar([FromBody] MenuRolVista menuRol)
    {
        var respuesta = await _configuracionService.AddMenuRolAsync(menuRol);
        return Ok(respuesta);
    }

    [HttpPut("MenuRolEditar")]
    public async Task<IActionResult> MenuRolEditar([FromBody] MenuRolVista menuRol)
    {
        var respuesta = await _configuracionService.UpdateMenuRolAsync(menuRol);
        return Ok(respuesta);
    }

    [HttpDelete("MenuRolEliminar/{menuRolId}")]
    public async Task<IActionResult> MenuRolEliminar(int menuRolId)
    {
        var respuesta = await _configuracionService.DeleteMenuRolAsync(menuRolId);
        return Ok(respuesta);
    }
    #endregion

    #region Accion
    [HttpPost("AccionObtener")]
    public IActionResult AccionObtener([FromBody] AccionVista accionRequest)
    {
        var respuesta = _configuracionService.AccionActivo(accionRequest.Id);
        return Ok(respuesta);
    }

    [HttpPost("AccionInsertar")]
    public async Task<IActionResult> AccionInsertar([FromBody] AccionVista accion)
    {
        var respuesta = await _configuracionService.AddAccionAsync(accion);
        return Ok(respuesta);
    }

    [HttpPut("AccionEditar")]
    public async Task<IActionResult> AccionEditar([FromBody] AccionVista accion)
    {
        var respuesta = await _configuracionService.UpdateAccionAsync(accion);
        return Ok(respuesta);
    }

    [HttpDelete("AccionEliminar/{accionId}")]
    public async Task<IActionResult> AccionEliminar(int accionId)
    {
        var respuesta = await _configuracionService.DeleteAccionAsync(accionId);
        return Ok(respuesta);
    }
    #endregion

    #region Servicio
    [HttpPost("ServicioObtener")]
    public IActionResult ServicioObtener([FromBody] ServicioVista servicioRequest)
    {
        var respuesta = _configuracionService.ServicioActivo(servicioRequest.Id);
        return Ok(respuesta);
    }

    [HttpPost("ServicioInsertar")]
    public async Task<IActionResult> ServicioInsertar([FromBody] ServicioVista servicio)
    {
        var respuesta = await _configuracionService.AddServicioAsync(servicio);
        return Ok(respuesta);
    }

    [HttpPut("ServicioEditar")]
    public async Task<IActionResult> ServicioEditar([FromBody] ServicioVista servicio)
    {
        var respuesta = await _configuracionService.UpdateServicioAsync(servicio);
        return Ok(respuesta);
    }

    [HttpDelete("ServicioEliminar/{servicioId}")]
    public async Task<IActionResult> ServicioEliminar(int servicioId)
    {
        var respuesta = await _configuracionService.DeleteServicioAsync(servicioId);
        return Ok(respuesta);
    }
    #endregion
}