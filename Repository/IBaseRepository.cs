using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository
{
    public interface IBaseRepository<T> where T : class
    {
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IList<T> List();
        T FindById(int id);
    }
}
