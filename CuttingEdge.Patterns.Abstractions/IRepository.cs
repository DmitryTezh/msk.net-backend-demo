using System.Linq;
using System.Threading.Tasks;

namespace CuttingEdge.Patterns.Abstractions
{
    /// <summary>
    /// Interface for repository of domain entities.
    /// </summary>
    /// <typeparam name="TEntity">Type for domain entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : class, IDomain
    {
        /// <summary>
        /// Query entities from repository.
        /// </summary>
        IQueryable<TEntity> Entities { get; }

        /// <summary>
        /// Get entity from repository by key.
        /// </summary>
        /// <param name="key">Entity key.</param>
        /// <returns>Entity.</returns>
        TEntity GetById(object key);

        /// <summary>
        /// Get entity from repository by key asynchronously.
        /// </summary>
        /// <param name="key">Entity key.</param>
        /// <returns>Entity.</returns>
        Task<TEntity> GetByIdAsync(object key);

        /// <summary>
        /// Insert new entity to repository.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Attach entity to repository.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Attach(TEntity entity);

        /// <summary>
        /// Update entity in repository.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete entity from repository.
        /// </summary>
        /// <param name="entity">Entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete entity from repository by key.
        /// </summary>
        /// <param name="entity">Entity key.</param>
        void Delete(object key);

        /// <summary>
        /// Delete entity from repository by key asynchronously.
        /// </summary>
        /// <param name="entity">Entity key.</param>
        Task DeleteAsync(object key);
    }
}
