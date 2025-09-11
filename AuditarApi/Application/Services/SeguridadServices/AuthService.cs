using Application.Services.SeguridadServices;
using Dominio.Entities;
using Dominio.ModuloSeguridad.Repositorio;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

public class AuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly PasswordService _passwordService;

    public AuthService(IUsuarioRepository usuarioRepository, PasswordService passwordService)
    {
        _usuarioRepository = usuarioRepository;
        _passwordService = passwordService;
    }

    public async Task<RespuestaApp<LoginResponseDto>> ValidarCredenciales(string correo, string password)
    {
        var respuesta = new RespuestaApp<LoginResponseDto>();
        try
        {
            bool mensaje = false;

            var usuario = await _usuarioRepository.GetByCorreoAsync(correo);

            if (usuario == null)
            {
                mensaje = true;
            } 
            else if (!_passwordService.VerifyPassword(password, usuario.PasswordUsuario))
            {
                mensaje = true;
            }

            if (!mensaje)
            {
                // Construir LoginResponseDto con los datos del usuario
                var usuarioVista = new LoginResponseDto
                {
                    Id = usuario.UsuarioId,
                    NombreCompleto = usuario.NombreUsuario + ' ' + usuario.ApellidoUsuario,
                    Rol = usuario.RolId.HasValue ? usuario.RolId.Value.ToString() : string.Empty,
                    Correo = usuario.CorreoUsuario,
                    Token = ""
                };

                respuesta.Vista = new List<LoginResponseDto> { usuarioVista };
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            { 
                respuesta.Mensaje = "Usuario o credenciales invalidos";
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.Mensaje = ex.Message.ToString();
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
        }
        return respuesta;
    }

    public string GenerarToken(string correo, int usuarioId, string rolNombre, IConfiguration configuration)
    {
        var jwtKey = configuration["Jwt:Key"];
        var jwtIssuer = configuration["Jwt:Issuer"];

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, correo),
            new Claim("usuarioId", usuarioId.ToString()),
            new Claim("rol", rolNombre ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: null,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}