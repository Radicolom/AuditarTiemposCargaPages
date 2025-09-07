using Dominio.Data;
using Dominio.Entities;
using Dominio.ModuloSeguridad.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Usuario> GetByCorreoAsync(string correo)
    {
        #pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
        return await _context.Usuarios
                             .Include(u => u.Rol)
                             .FirstOrDefaultAsync(u => u.CorreoUsuario == correo);
        #pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
    }
}
