using Repository.MD;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Domain;

namespace Repository.NewsRepository
{
    public interface INewsRepository
    {
        bool Insert(News entity);
        bool Update(News entity);
        bool Delete(News entity);
        IList<News> List();
        News FindById(int id);
    }

    public class NewsRepositoryMB : MongoRepositoryBase<News>, INewsRepository
    {
    }
    public class NewsRepositoryEF : EntityRepositoryBase<News>, INewsRepository
    {
    }
}
