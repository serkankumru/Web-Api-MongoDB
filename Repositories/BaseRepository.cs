using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDAL.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace MongoDAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IRepository<TEntity> where TEntity : EntityBase
    {
        public bool Insert(TEntity entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            return Collection.Insert(entity).Response.ToBoolean();
        }

        public bool Update(TEntity entity)
        {
            if (entity.Id == null)
            {
                return Insert(entity);
            }
            return Collection.Save(entity).DocumentsAffected > 0;
        }

        public bool Delete(TEntity entity)
        {
            return Collection.Remove(Query.EQ("_id", entity.Id)).DocumentsAffected > 0;
        }

        public IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable<TEntity>().Where(predicate.Compile()).ToList();
        }

        public IList<TEntity> GetAll()
        {
            return Collection.FindAllAs<TEntity>().ToList();
        }

        public TEntity GetById(string id)
        {
            return Collection.FindOneByIdAs<TEntity>(id);
        }
    }
}