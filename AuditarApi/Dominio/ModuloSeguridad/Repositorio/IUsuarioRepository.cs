using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ModuloSeguridad.Repositorio
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByCorreoAsync(string correo);
        List<Usuario> GetByUsuarioAsync(bool? estado);
        Task<Usuario> AddUsuarioAsync(Usuario usuario);
        Task<Usuario> UpdateUsuarioAsync(Usuario usuario);
        Task<bool> DeleteUsuarioAsync(int usuarioId);
    }
}
