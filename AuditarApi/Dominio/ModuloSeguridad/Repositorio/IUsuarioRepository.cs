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
    }
}
