using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ModuloConfiguracion.Repositorio
{
    public interface IMenuRolRepository
    {
        List<MenuRol> GetByMenuRolAsync(int? menuId = null, int? rolId = null);
        Task<MenuRol> AddMenuRolAsync(MenuRol menuRol);
        Task<MenuRol> UpdateMenuRolAsync(MenuRol menuRol);
        Task<bool> DeleteMenuRolAsync(int menuRolId);
    }
}
