using Dominio.ModuloSeguridad.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        var usuario = await _authService.ValidarCredenciales(loginRequest.Correo, loginRequest.Password);

        if (usuario == null)
        {
            return Unauthorized(new { message = "Credenciales inválidas." });
        }

        // ... Generar token y respuesta ...
        var response = new LoginResponseDto
        {
            Id = usuario.UsuarioId,
            NombreCompleto = $"{usuario.NombreUsuario} {usuario.ApellidoUsuario}",
            Rol = usuario.Rol?.NombreRol ?? string.Empty,
            Token = "TOKEN_DE_EJEMPLO_TEMPORAL" // Próximo paso: Generar un JWT real
        };

        return Ok(response);
    }
}