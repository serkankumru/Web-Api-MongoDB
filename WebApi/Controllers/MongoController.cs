using Repository.NewsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using WebApi.Model;
using Domain;

namespace WebApi.Controllers
{
    public class MongoController : ApiController
    {
        INewsRepository _repoMD;

        public MongoController()
        {
            _repoMD = new NewsRepositoryMB();            
        }
        //[OutputChache]
        [Route("api/mongo/list")]
        public IHttpActionResult Get()
        {
            return Json(_repoMD.List().Select(x => new NewsDTO() { Title = x.Title, Text = x.Text, CreateDate = x.CreateDate.Value }));
        }

        [Route("api/mongo/list")]
        public IHttpActionResult Get(int id)
        {
            News news = _repoMD.FindById(id);
            if (news != null)
                return Json((new NewsDTO() { Title = news.Title, Text = news.Text, CreateDate = news.CreateDate.Value }));
            else
                return Json("null");
        }
    }
}
