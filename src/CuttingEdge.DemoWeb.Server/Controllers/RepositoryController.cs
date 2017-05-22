using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CollectionJson;
using CuttingEdge.Patterns.Abstractions;
using CuttingEdge.Patterns.View;
using CuttingEdge.DemoWeb.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuttingEdge.DemoWeb.Server.Controllers
{
    public abstract class RepositoryController<TEntity> : Controller where TEntity : Domain
    {
        protected IUnitOfWork<Domain> UoW { get; }
        protected IRepositoryFactory<Domain> Repositories { get; }
        protected IRepository<TEntity> Repository { get; }
        protected IViewManager<EntityView> ViewManager { get; }

        public RepositoryController(IUnitOfWork<Domain> uof, IRepositoryFactory<Domain> repos, IViewManager<EntityView> viewer)
        {
            UoW = uof ?? throw new ArgumentException(nameof(uof));
            Repositories = repos ?? throw new ArgumentException(nameof(repos));
            Repository = Repositories.Create<TEntity>(UoW);
            ViewManager = viewer ?? throw new ArgumentNullException(nameof(viewer));
        }

        protected ReadDocument GetCollectionJson<T>(T item) where T : Domain
        {
            var items = new List<T> { item };
            return GetCollectionJson(items.AsEnumerable());
        }

        protected ReadDocument GetCollectionJson<T>(IEnumerable<T> items) where T : Domain
        {
            var route = RouteData.Values["controller"].ToString();
            var view = ViewManager.GetFor<T>("@", route, "*");
            var props = from propView in view.AttributeViews
                        from propType in typeof(T).GetProperties()
                        where propView.Name == propType.Name
                        select new
                        {
                            View = propView,
                            Type = propType
                        };

            var collection = new Collection
            {
                Version = "1.0",
                Href = new Uri(Url.Link(typeof(T).Name, null))
            };
            items.ToList().ForEach(item => collection.Items.Add(new Item
            {
                Href = new Uri(Url.Link(typeof(T).Name, new { id = item.Id })),
                Data = props.Select(prop => new Data()
                {
                    Name = prop.View.Name,
                    Prompt = prop.View.Prompt,
                    Value = String.Format(prop.View.Format ?? "{0}", prop.Type.GetValue(item))
                }).ToList()
            }));
            return new ReadDocument { Collection = collection };
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
