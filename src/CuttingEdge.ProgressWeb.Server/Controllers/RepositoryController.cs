using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using CollectionJson;
using CuttingEdge.Patterns.Abstractions;
using CuttingEdge.ProgressWeb.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuttingEdge.ProgressWeb.Server.Controllers
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

        protected ReadDocument GetCollectionJson<T>(T item) where T : Domain
        {
            var items = new List<T> { item };
            return GetCollectionJson(items.AsEnumerable());
        }

        protected ReadDocument GetCollectionJson<T>(IEnumerable<T> items) where T : Domain
        {
            var collection = new Collection
            {
                Version = "1.0",
                Href = new Uri(Url.Link(typeof(T).Name, null))
            };
            var props = from prop in typeof(T).GetProperties()
                        let requiredAttr = (RequiredAttribute)Attribute.GetCustomAttribute(prop, typeof(RequiredAttribute))
                        let displayAttr = (DisplayAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayAttribute))
                        select new
                        {
                            Type = prop,
                            Name = prop.Name,
                            Label = displayAttr?.Name ?? prop.Name,
                            Prompt = displayAttr?.Prompt,
                            IsRequired = requiredAttr != null
                        };
            items.ToList().ForEach(item => collection.Items.Add(new Item
            {
                Href = new Uri(Url.Link(typeof(T).Name, new { id = item.Id })),
                Data = props.Select(prop => new Data()
                {
                    Name = prop.Name,
                    Prompt = prop.Prompt,
                    Value = prop.Type.GetValue(item)?.ToString()
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
