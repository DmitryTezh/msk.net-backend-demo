using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CuttingEdge.Patterns.Abstractions;
using CuttingEdge.ProgressWeb.Entity;

namespace CuttingEdge.ProgressWeb.Server.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private IUnitOfWork<Domain> _uof;
        private IRepository<Comment> _repo;

        public CommentController(IUnitOfWork<Domain> uof, IRepositoryFactory<Domain> repos)
        {
            _uof = uof ?? throw new ArgumentException(nameof(uof));
            _repo = (repos ?? throw new ArgumentException(nameof(repos))).Create<Comment>(_uof);
        }

        // GET api/values
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Get()
        {
            return Json(_repo.Entities.OrderBy(r => r.Author).ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Get(int id)
        {
            return Json(_repo.GetById(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Comment value)
        {
            _repo.Insert(value);
            _uof.Save();

            return Json(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Comment value)
        {
            _repo.Update(value);
            _uof.Save();

            return Json(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.Delete(id);
            _uof.Save();
        }
    }
}
