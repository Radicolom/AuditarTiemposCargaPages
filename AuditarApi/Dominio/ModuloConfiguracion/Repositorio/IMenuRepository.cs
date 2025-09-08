using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ModuloConfiguracion.Repositorio
{
    public interface IMenuRepository
    {
        List<Menu> GetByMenuAsync(bool? EstadoMenu = null);
    }
}
