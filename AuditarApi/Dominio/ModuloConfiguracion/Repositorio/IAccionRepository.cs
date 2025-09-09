using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ModuloConfiguracion.Repositorio
{
    public interface IAccionRepository
    {
        List<Accion> GetByAccionAsync(int? menuId = null, int? accionId = null);
        Task<Accion> AddAccionAsync(Accion menuAccion);
        Task<Accion> UpdateAccionAsync(Accion menuAccion);
        Task<bool> DeleteAccionAsync(int menuAccionId);
    }
}