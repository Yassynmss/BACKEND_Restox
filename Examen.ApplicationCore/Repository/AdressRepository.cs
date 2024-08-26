//using Examen.ApplicationCore.Domain;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Examen.ApplicationCore.Repository
//{
//    public class AdressRepository : IAdressRepository
//    {
//        private readonly DbContext _context;
//        private readonly DbSet<Adress> _dbSet;

//        public AdressRepository(DbContext context)
//        {
//            _context = context;
//            _dbSet = _context.Set<Adress>();
//        }

//        public IEnumerable<Adress> GetAll()
//        {
//            return _dbSet.ToList();
//        }

//        public Adress GetById(int id)
//        {
//            return _dbSet.Find(id);
//        }

//        public IEnumerable<Adress> GetMany(Func<Adress, bool> predicate)
//        {
//            return _dbSet.Where(predicate).ToList();
//        }

//        public void Add(Adress entity)
//        {
//            _dbSet.Add(entity);
//        }

//        public void Update(Adress entity)
//        {
//            _dbSet.Update(entity);
//        }

//        public void Delete(Adress entity)
//        {
//            _dbSet.Remove(entity);
//        }
//    }
//}
