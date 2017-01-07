using System;
using Microsoft.EntityFrameworkCore;
using CuttingEdge.Patterns.Abstractions;

namespace CuttingEdge.Patterns.Repository
{
    public class UnitOfWorkFactory<TDomain> : IUnitOfWorkFactory<TDomain> where TDomain : class, IDomain
    {
        private readonly DbContextOptions<EntityContext<TDomain>> _options;

        public UnitOfWorkFactory(DbContextOptions<EntityContext<TDomain>> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public IUnitOfWork<TDomain> Create()
        {
            return new UnitOfWork<TDomain>(_options);
        }
    }
}
