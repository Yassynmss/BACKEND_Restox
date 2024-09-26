using AM.ApplicationCore.Interfaces;
using Examen.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IServiceMenu : IService<Menu>
    {
        ApplicationUser GetApplicationUserById(string ApplicationUserID);
        Task<List<Menu>> GetAllMenusWithUsersAsync();
        Task<Menu> GetMenuByIdWithUserAsync(int menuId);

    }
}
