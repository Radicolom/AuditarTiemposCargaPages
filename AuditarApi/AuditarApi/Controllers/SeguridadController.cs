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









}