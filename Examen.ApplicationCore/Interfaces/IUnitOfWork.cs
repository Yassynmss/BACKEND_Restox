
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();

        int Save();
        void Commit();
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}
