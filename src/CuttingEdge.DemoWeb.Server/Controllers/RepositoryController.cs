using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CuttingEdge.Patterns.Abstractions;
using CuttingEdge.DemoWeb.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuttingEdge.DemoWeb.Server.Controllers
{
    public abstract class RepositoryController<TEntity> : Controller where TEntity : Domain
    {
        protected IUnitOfWork<Domain> UoW { get; }
        protected IRepositoryFactory<Domain> Repositories { get; }
        protected IRepository<TEntity> Repository { get; }

        public RepositoryController(IUnitOfWork<Domain> uof, IRepositoryFactory<Domain> repos)
        {
            UoW = uof ?? throw new ArgumentException(nameof(uof));
            Repositories = repos ?? throw new ArgumentException(nameof(repos));
            Repository = Repositories.Create<TEntity>(UoW);
        }

        protected void SaveChanges()
        {
            UoW.Save();
        }

        protected async Task SaveChangesAsync()
        {
            await UoW.SaveAsync();
        }
    }
}
