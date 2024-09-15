using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;

namespace Examen.ApplicationCore.Services
{
    public class ServiceMenu : Service<Menu>, IServiceMenu
    {
        public ServiceMenu(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public BizAccount GetBizAccountById(string bizAccountId)
        {
            // Utilisez _unitOfWork pour accéder au repository
            return _unitOfWork.Repository<BizAccount>().GetById(bizAccountId);
        }
    }
}
