using Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository.MD
{
    public class MongoRepositoryBase<T> : IBaseRepository<T> where T : News
    {
        protected static MongoDatabase Database { get; set; }
        protected static MongoCollection<T> Collection { get; set; }
        protected static int lastId { get; set; }
        //app.configden veri database name ve connection string bind
        string _dbName = ConfigurationManager.AppSettings["MongoDbDatabaseName"];
        string _conn = ConfigurationManager.AppSettings["MongoDbConnectionString"];

        public MongoRepositoryBase()
        {
            if (Database == null || Collection == null)
            {
                if (_conn == null || _dbName == null)
                {
                    _dbName = "News";
                    _conn = "mongodb://localhost/News?safe=true";
                }
                var client = new MongoClient(_conn);
                var server = client.GetServer();

                Database = server.GetDatabase(_dbName);
                Collection = Database.GetCollection<T>(typeof(T).Name);
                lastId = Collection.AsQueryable().Count();
            }
        }

        public bool Insert(T entity)
        {
            entity.Id = lastId != 0 ? Collection.AsQueryable().OrderByDescending(l => l.Id).First().Id + 1 : 1;
            lastId = 1;
            return Collection.Insert(entity).Response.ToBoolean();
        }

        public bool Update(T entity)
        {
            if (entity.Id == null)
            {
                return Insert(entity);
            }
            return Collection.Save(entity).DocumentsAffected > 0;
        }

        public bool Delete(T entity)
        {
            return Collection.Remove(Query.EQ("_id", entity.Id)).DocumentsAffected > 0;
        }

        public IList<T> List()
        {
            return Collection.FindAllAs<T>().ToList();
        }

        public T FindById(int id)
        {
            return Collection.FindOneByIdAs<T>(id);
        }
    }
}
