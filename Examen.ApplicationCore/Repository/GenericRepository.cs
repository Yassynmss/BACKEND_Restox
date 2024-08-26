//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace Examen.ApplicationCore.Repository
//{
//    public class GenericRepository<T> : IGenericRepository<T> where T : class
//    {
//        private readonly DbContext _context;
//        private readonly DbSet<T> _dbSet;

//        public GenericRepository(DbContext context)
//        {
//            _context = context;
//            _dbSet = _context.Set<T>();
//        }

//        public IEnumerable<T> GetAll()
//        {
//            return _dbSet.ToList();
//        }

//        public T GetById(int id)
//        {
//            return _dbSet.Find(id);
//        }

//        public IEnumerable<T> GetMany(Expression<Func<T, bool>> filter)
//        {
//            return _dbSet.Where(filter).ToList();
//        }

//        public void Add(T entity)
//        {
//            _dbSet.Add(entity);
//        }

//        public void Update(T entity)
//        {
//            _dbSet.Update(entity);
//        }

//        public void Delete(T entity)
//        {
//            _dbSet.Remove(entity);
//        }
//    }
//}