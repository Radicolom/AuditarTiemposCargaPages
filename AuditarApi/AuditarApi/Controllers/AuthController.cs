using Dominio.ModuloSeguridad.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(AuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        var usuario = await _authService.ValidarCredenciales(loginRequest.Correo, loginRequest.Password);

        if (usuario.OperacionExitosa && !usuario.ValidacionesNegocio)
        {
            var user = usuario.Vista[0];
            user.Token = _authService.GenerarToken(user.Correo, user.Id, user.Rol, _configuration);
            return Ok( usuario );
        }
        else
        {
            return Unauthorized(new { message = usuario.Mensaje });
        }
    }

    // --- ENDPOINT PROTEGIDO DE EJEMPLO ---
    // Mantenemos este endpoint para que puedas probar que la autenticación con JWT funciona.
    [HttpGet("protegido")]
    [Authorize] // Para acceder aquí, se necesita un token JWT válido.
    public IActionResult GetProtectedData()
    {
        var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        return Ok(new { message = $"Hola {userEmail}, tienes acceso a esta información secreta." });
    }
}