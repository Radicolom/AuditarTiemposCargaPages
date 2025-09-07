using Dominio.Entities;
using Dominio.ModuloSeguridad.Repositorio;

public class AuthService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario> ValidarCredenciales(string correo, string password)
    {
        var usuario = await _usuarioRepository.GetByCorreoAsync(correo);

        if (usuario == null)
        {
            return null;
        }

        // 3. Verificar la contraseña (lógica de hashing pendiente)
        if (usuario.PasswordUsuario != password)
        {
            return null;
        }

        return usuario;
    }
}