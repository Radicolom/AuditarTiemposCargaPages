using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ModuloSeguridad.Repositorio
{
    public interface IRolOperacionAccionRepository
    {
        List<RolOperacionAccion> GetByRolOperacionAccionAsync(int? rolId = null, int? operacionId = null, int? accionId = null);
        Task<RolOperacionAccion> AddRolOperacionAccionAsync(RolOperacionAccion rolOperacionAccion);
        Task<RolOperacionAccion> UpdateRolOperacionAccionAsync(RolOperacionAccion rolOperacionAccion);
        Task<bool> DeleteRolOperacionAccionAsync(int rolOperacionAccionId);
    }
}