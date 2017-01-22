using System;
using System.Threading.Tasks;

namespace CuttingEdge.Patterns.Abstractions
{
    public interface IUnitOfWork<TDomain> : IDisposable where TDomain: class
    {
        /// <summary>
        /// Save changes in repository.
        /// </summary>
        void Save();

        /// <summary>
        /// Save changes in repository asynchronously.
        /// </summary>
        Task SaveAsync();
    }
}
