using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ModuloSeguridad.Repositorio
{
    public interface IRolRepository
    {
        List<Rol> GetByRolAsync(bool? EstadoRol = null);
        Task<Rol> AddRolAsync(Rol rol);
        Task<Rol> UpdateRolAsync(Rol rol);
        Task<bool> DeleteRolAsync(int rolId);
    }
}
