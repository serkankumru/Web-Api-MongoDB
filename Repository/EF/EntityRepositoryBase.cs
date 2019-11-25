using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EF
{
    public class EntityRepositoryBase<T> : IBaseRepository<T> where T : News
    {
        protected NewsApiDBEntities _context;
        public EntityRepositoryBase()
        {
            if (_context == null)
                _context = NewsApiDBEntities.CreateInstanceSingleton();
        }


        public bool Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool Update(T entity)
        {
            if (entity == null)
                return false;

            T exist = _context.Set<T>().Find(entity.Id);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                return _context.SaveChanges() == 1 ? true : false;
            }
            return false;
        }

        public bool Delete(T entity)
        {
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                return _context.SaveChanges() == 1 ? true : false;
            }
            return false;
        }

        public IList<T> List()
        {
            return _context.Set<T>().ToList();
        }

        public T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T LastInserted()
        {
            return _context.Set<T>().OrderByDescending(x => x.Id).FirstOrDefault();
        }
    }
}
