//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace Examen.ApplicationCore.Repository
//{
//    public interface IGenericRepository<T> where T : class
//    {
//        IEnumerable<T> GetAll();
//        T GetById(int id);
//        IEnumerable<T> GetMany(Expression<Func<T, bool>> filter);
//        void Add(T entity);
//        void Update(T entity);
//        void Delete(T entity);
//    }
//}
