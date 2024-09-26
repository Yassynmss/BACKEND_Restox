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
    public class ServiceItem : Service<Item>, IServiceItem
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceItem(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork; // Assurez-vous de garder une référence à unitOfWork
        }

        public async Task CommitAsync()
        {
            // Implémentez ici la logique pour enregistrer les changements de manière asynchrone.
            await _unitOfWork.SaveChangesAsync(); // Utilisez une méthode pour enregistrer les changements.
        }
    }
}
