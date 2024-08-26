using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Examen.ApplicationCore.Services
{
    public class ServiceAdress : Service<Adress>, IServiceAdress
    {
        // private readonly IGenericRepository<Adress> _repository;
        //private readonly IUnitOfWork _unitOfWork;

        // Constructeur avec injection de dépendance pour IGenericRepository et IUnitOfWork
        //    public ServiceAdress(IGenericRepository<Adress> repository, IUnitOfWork unitOfWork) : base(unitOfWork)
        //   {
        //       _repository = repository;
        //       _unitOfWork = unitOfWork;
        //   }

        /*      // Méthode pour éditer les adresses par Line1
              public void EditAdressByLine1(string oldLine1, string newLine1)
              {
                  var addresses = _repository.GetMany(a => a.Line1 == oldLine1);
                  foreach (var address in addresses)
                  {
                      address.Line1 = newLine1;
                      _repository.Update(address); // Mise à jour individuelle
                  }
                  // Valider les changements avec UnitOfWork.Commit()
                  _unitOfWork.Commit();
              }

              // Méthode pour récupérer toutes les valeurs de Line1
              public IEnumerable<string> GetAllLine1()
              {
                  return _repository.GetAll().Select(a => a.Line1);
              }*/
        public ServiceAdress(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
