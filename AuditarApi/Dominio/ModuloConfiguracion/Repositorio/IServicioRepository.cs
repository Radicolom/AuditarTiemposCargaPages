using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ModuloConfiguracion.Repositorio
{
    public interface IServicioRepository
    {
        List<Servicio> GetByServicioAsync(int? servicioId = null);
        Task<Servicio> AddServicioAsync(Servicio servicio);
        Task<Servicio> UpdateServicioAsync(Servicio servicio);
        Task<bool> DeleteServicioAsync(int servicioId);
    }
}
