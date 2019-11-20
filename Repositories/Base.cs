using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDAL.Repositories
{
    public class IBaseRepository<TEntity>
    {
        protected static MongoDatabase Database { get; set; }
        protected static MongoCollection<TEntity> Collection { get; set; }

        //app.configden veri database name ve connection string bind
        string _dbName = ConfigurationManager.AppSettings["MongoDbDatabaseName"];
        string _conn = ConfigurationManager.AppSettings["MongoDbConnectionString"];

        public IBaseRepository()
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
                Collection = Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }
    }
}
