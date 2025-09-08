using Application.Services.SeguridadServices;
using Dominio.Entities;
using Dominio.ModuloSeguridad.Repositorio;

public class AuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly PasswordService _passwordService;

    public AuthService(IUsuarioRepository usuarioRepository, PasswordService passwordService)
    {
        _usuarioRepository = usuarioRepository;
        _passwordService = passwordService;
    }

    public async Task<Usuario> ValidarCredenciales(string correo, string password)
    {
        var usuario = await _usuarioRepository.GetByCorreoAsync(correo);

        if (usuario == null)
        {
            return null;
        }

        if (!_passwordService.VerifyPassword(password, usuario.PasswordUsuario))
        {
            return null;
        }

        return usuario;
    }
}