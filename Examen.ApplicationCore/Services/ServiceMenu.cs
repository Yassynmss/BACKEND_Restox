using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Examen.ApplicationCore.Services
{
    public class ServiceMenu : Service<Menu>, IServiceMenu
    {
        public ServiceMenu(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ApplicationUser GetApplicationUserById(string applicationUserId)
        {
            // Use _unitOfWork to access the repository
            return _unitOfWork.Repository<ApplicationUser>().GetById(applicationUserId);
        }

        public async Task<List<Menu>> GetAllMenusWithUsersAsync()
        {
            return await _unitOfWork.Repository<Menu>()
                .Query() // Assuming Query() returns an IQueryable<Menu>
                .Include(m => m.ApplicationUser) // Include the user (chef) relationship
                .ToListAsync(); // Fetch all menus with their corresponding ApplicationUser
        }
        public async Task<Menu> GetMenuByIdWithUserAsync(int menuId)
        {
            return await _unitOfWork.Repository<Menu>()
                .Query() // Assuming Query() returns an IQueryable<Menu>
                .Include(m => m.ApplicationUser) // Ensure that this relationship is set up in your model
                .FirstOrDefaultAsync(m => m.MenuID == menuId);
        }


    }
}
