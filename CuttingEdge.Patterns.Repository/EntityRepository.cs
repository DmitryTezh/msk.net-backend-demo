using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CuttingEdge.Patterns.Abstractions;

namespace CuttingEdge.Patterns.Repository
{
    public class EntityRepository<TEntity, TDomain> : IRepository<TEntity> 
        where TEntity : class, IDomain
        where TDomain : class, IDomain
    {
        private readonly EntityContext<TDomain> _entityContext;
        private DbSet<TEntity> _entitySet;

        public EntityRepository(UnitOfWork<TDomain> unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            _entityContext = unitOfWork.EntityContext;
        }

        public IQueryable<TEntity> Entities
        {
            get
            {
                return EntitySet.AsQueryable();
            }
        }

        public TEntity GetById(object key)
        {
            return EntitySet.Find(key);
        }

        public async Task<TEntity> GetByIdAsync(object key)
        {
            return await EntitySet.FindAsync(key);
        }

        public void Insert(TEntity entity)
        {
            EntitySet.Add(entity);
        }

        public void Attach(TEntity entity)
        {
            EntitySet.Attach(entity);
        }

        public void Update(TEntity entity)
        {
            EntitySet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            EntitySet.Remove(entity);
        }

        public void Delete(object key)
        {
            var entity = GetById(key);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public async Task DeleteAsync(object key)
        {
            var entity = await GetByIdAsync(key);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        private DbSet<TEntity> EntitySet
        {
            get
            {
                if (_entitySet == null)
                {
                    _entitySet = _entityContext.Set<TEntity>();
                }
                return _entitySet;
            }
        }
    }
}
