using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CuttingEdge.Patterns.Abstractions;

namespace CuttingEdge.Patterns.Repository
{
    public class UnitOfWork<TDomain> : IUnitOfWork<TDomain> where TDomain : class
    {
        private bool disposed;

        public EntityContext<TDomain> EntityContext { get; }

        public UnitOfWork(DbContextOptions<EntityContext<TDomain>> options)
        {
            EntityContext = new EntityContext<TDomain>(options);
        }

        public void Save()
        {
            EntityContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await EntityContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    EntityContext.Dispose();
                }
                disposed = true;
            }
        }
    }
}
