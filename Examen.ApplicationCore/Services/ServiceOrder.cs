using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class ServiceOrder : Service<Order>, IServiceOrder
    {
        // Déclaration du champ privé
        private readonly IUnitOfWork _unitOfWork;

        public ServiceOrder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork; // Initialisation du champ
        }

        // Méthode GetTotalOrders
        public int GetTotalOrders()
        {
            return _unitOfWork.Repository<Order>().GetAll().Count();
        }
    }
}
