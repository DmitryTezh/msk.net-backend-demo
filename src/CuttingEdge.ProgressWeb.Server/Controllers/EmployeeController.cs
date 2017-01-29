using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollectionJson;
using CuttingEdge.Patterns.Abstractions;
using CuttingEdge.Patterns.View;
using CuttingEdge.ProgressWeb.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuttingEdge.ProgressWeb.Server.Controllers
{
    [Route("api/[controller]", Name = nameof(Employee))]
    public class EmployeeController : RepositoryController<Employee>
    {
        public EmployeeController(IUnitOfWork<Domain> uof, IRepositoryFactory<Domain> repos, IViewManager<EntityView> viewer) : base(uof, repos, viewer)
        {
        }

        // GET: api/values
        [HttpGet]
        public async Task<ReadDocument> Get()
        {
            var items = await Repository.Entities.ToListAsync();
            return GetCollectionJson(items.AsEnumerable());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ReadDocument> Get(long id)
        {
            var item = await Repository.GetByIdAsync(id);
            return GetCollectionJson(item);
        }
    }
}
