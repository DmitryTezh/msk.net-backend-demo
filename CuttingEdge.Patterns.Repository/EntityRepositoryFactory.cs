using CuttingEdge.Patterns.Abstractions;

namespace CuttingEdge.Patterns.Repository
{
    public class EntityRepositoryFactory<TDomain> : IRepositoryFactory<TDomain> where TDomain : class, IDomain
    {
        public IRepository<TEnity> Create<TEnity>(IUnitOfWork<TDomain> unitOfWork) where TEnity : class, TDomain
        {
            return new EntityRepository<TEnity, TDomain>((UnitOfWork<TDomain>)unitOfWork);
        }
    }
}
