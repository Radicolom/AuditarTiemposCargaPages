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









}