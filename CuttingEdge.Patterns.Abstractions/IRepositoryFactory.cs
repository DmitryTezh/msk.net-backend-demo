namespace CuttingEdge.Patterns.Abstractions
{
    public interface IRepositoryFactory<TDomain> where TDomain: class, IDomain
    {
        IRepository<TEnity> Create<TEnity>(IUnitOfWork<TDomain> context) where TEnity: class, TDomain;
    }
}
