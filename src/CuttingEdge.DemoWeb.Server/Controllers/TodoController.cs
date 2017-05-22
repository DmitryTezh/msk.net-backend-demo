using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CuttingEdge.Patterns.Abstractions;
using CuttingEdge.Patterns.View;
using CuttingEdge.DemoWeb.Entity;

namespace CuttingEdge.DemoWeb.Server.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : RepositoryController<TodoItem>
    {
        public TodoController(IUnitOfWork<Domain> uof, IRepositoryFactory<Domain> repos, IViewManager<EntityView> viewer) : base(uof, repos, viewer)
        {
        }

        // GET api/values
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Get()
        {
            return Json(Repository.Entities.OrderBy(r => r.Priority).ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Get(long id)
        {
            return Json(Repository.GetById(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(TodoItem value)
        {
            Repository.Insert(value);
            SaveChanges();

            return Json(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]TodoItem value)
        {
            Repository.Update(value);
            SaveChanges();

            return Json(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            Repository.Delete(id);
            SaveChanges();
        }
    }
}
