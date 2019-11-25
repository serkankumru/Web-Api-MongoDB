using Domain;
using Repository.NewsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/entity/list")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EntityController : ApiController
    {
        INewsRepository _repoEF;

        public EntityController()
        {
            _repoEF = new NewsRepositoryEF();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(_repoEF.List().Select(x => new NewsDTO() { Title = x.Title, Text = x.Text, CreateDate = x.CreateDate.Value }));
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            News news = _repoEF.FindById(id);
            if (news != null)
                return Json((new NewsDTO() { Title = news.Title, Text = news.Text, CreateDate = news.CreateDate.Value }));
            else
                return Json("null");
        }
    }
}
