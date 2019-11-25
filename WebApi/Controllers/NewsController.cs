using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Web.Http.Cors;
using Repository.NewsRepository;
using Domain;

namespace NoSQL.Controllers
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/news/{action}")]
    public class NewsController : ApiController
    {
        NewsRepositoryEF _repoEF;
        NewsRepositoryMB _repoMD;

        public NewsController()
        {
            _repoEF = new NewsRepositoryEF();
            _repoMD = new NewsRepositoryMB();
        }

        public IHttpActionResult Post([ModelBinder]News entity)
        {
            if (entity == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            entity.CreateDate = DateTime.Now;
            string result = "SQL Server saved:" + _repoEF.Insert(entity).ToString().ToLower() + " MongoDb saved:" + _repoMD.Insert(entity).ToString().ToLower();
            return Json(result);
        }

        public IHttpActionResult Put([ModelBinder]News entity)
        {
            News entityEF = _repoEF.FindById(entity.Id);

            if (entity == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            entity.CreateDate = entityEF.CreateDate;
            string result = "MongoDb updated:" + _repoMD.Update(entity).ToString().ToLower() + " SQL Server updated:" + _repoEF.Update(entity).ToString().ToLower();
            return Json(result);
        }

        public IHttpActionResult Delete(int id)
        {
            News entity = _repoEF.FindById(id);

            string result = "MongoDb removed:" + _repoMD.Delete(entity).ToString().ToLower() + " SQL Server removed:" + _repoEF.Delete(entity).ToString().ToLower();
            return Json(result);
        }
    }
}
