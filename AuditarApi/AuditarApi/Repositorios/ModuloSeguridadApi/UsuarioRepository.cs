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

    public List<Usuario> GetByUsuarioAsync(bool? estado)
    {
        var query = _context.Usuarios.Include(u => u.Rol).AsQueryable();
        if (estado is not null)
        {
            query = query.Where(u => u.EstadoUsuario == estado);
        }
        return query.ToList();
    }

    public async Task<Usuario> AddUsuarioAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

        var resultado = await _context.Usuarios
                            .Include(u => u.Rol)
                            .FirstOrDefaultAsync(u => u.UsuarioId == usuario.UsuarioId);

        if (resultado == null) throw new InvalidOperationException("No se pudo agregar el usuario.");

        return resultado;
    }

    public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<bool> DeleteUsuarioAsync(int usuarioId)
    {
        var usuario = await _context.Usuarios.FindAsync(usuarioId);
        if (usuario == null)
            return false;

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
    }
}
