using AM.ApplicationCore.Interfaces;
using Examen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace AM.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExamenContext _context;
        private readonly Type repositoryType;
        private bool disposedValue;

        public UnitOfWork(ExamenContext context, Type type)
        {
            _context = context;
            repositoryType = type;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            return (IGenericRepository<T>)Activator.CreateInstance(repositoryType
                        .MakeGenericType(typeof(T)), _context);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}
