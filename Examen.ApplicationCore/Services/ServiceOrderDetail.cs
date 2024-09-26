using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;

namespace Examen.ApplicationCore.Services
{
    public class ServiceOrderDetail : Service<OrderDetail>, IServiceOrderDetail
    {
        public ServiceOrderDetail(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
